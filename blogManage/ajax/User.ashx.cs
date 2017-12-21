using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blogManage.baseClass;
using Utility;
using Newtonsoft.Json;
using System.Data;
using Microsoft.JScript;
using System.Text;
using System.Data.SqlClient;

namespace blogManage.ajax
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class User : BaseAshx
    {

        public override void ProcessRequest(HttpContext context)
        {
            action = Funcs.Get("action");
            //判断是否为登录请求
            if (action == "userLogin")
            {
                returnData = userLogin();
            }
            //判断是否未登录
            else if (!islogin())
            {
                returnData = returnLogin();
            }
            else
            {
                OperateAciton oa = new OperateAciton();
                oa.GatherOperate("getTypeList", getTypeList);               // 获取用户类型列表
                oa.GatherOperate("addOrEditUserType", addOrEditUserType);   // 添加或编辑用户类型
                oa.GatherOperate("deleteUserType", deleteUserType);         // 删除用户类型
                oa.GatherOperate("getUserList", getUserList);               // 获取用户列表
                oa.GatherOperate("getUserInfo", getUserInfo);               // 获取用户信息
                oa.GatherOperate("addOrEditUser", addOrEditUser);           // 新增或编辑用户
                oa.GatherOperate("deleteUser", deleteUser);                 // 删除用户
                oa.GatherOperate("changePwd", changePwd);                   // 修改密码
                oa.GatherOperate("getUserPower", getUserPower);             // 获得用户权限
                oa.GatherOperate("savePowers", savePowers);                 // 保存权限
                oa.GatherOperate("getMenuTree", getMenuTree);               // 绑定菜单
                oa.GatherOperate("getUserMenu", getUserMenu);               // 获取用户菜单

                returnData = oa.ExecuteOperate(action);
            }
            context.Response.Write(returnData);
        }

        // 用户登录
        private string userLogin()
        {
            myJson my = new myJson();
            try
            {
                #region 检测用户名，密码
                string userName = Funcs.Get("userName");//用户名
                string pwd = Funcs.Get("pwd");//密码

                string strSql = "select su.*,sp.paramsName userTypeName from system_users su inner join system_params sp on su.userType=sp.id where su.userName=@userName and su.password=@pwd";
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@userName",SqlDbType.VarChar){ Value = userName },
                    new SqlParameter("@pwd",SqlDbType.VarChar){ Value = Funcs.MD5(pwd) }
                };

                DataTable tb = Utility.SqlHelper.GetDataTable(strSql, param);
                if (tb == null || tb.Rows.Count < 1)
                {
                    my.flag = 0;
                    my.msg = "用户名或密码错误！";
                    return JsonConvert.SerializeObject(my);
                }
                if (int.Parse(tb.Rows[0]["userstatus"].ToString()) == 0)
                {
                    my.flag = 0;
                    my.msg = "您的帐号已暂停使用，请联系管理员！";
                    return JsonConvert.SerializeObject(my);
                }
                #endregion

                #region 保存用户信息，权限到Session
                string loginUserId = tb.Rows[0]["id"].ToString();
                //防止一个帐号多处登录
                Global.Add(int.Parse(loginUserId), HttpContext.Current.Session.SessionID);


                //保存用户的信息到Session
                MySession.Add("userId", tb.Rows[0]["id"]);
                MySession.Add("userName", tb.Rows[0]["userName"]);
                MySession.Add("userTypeName", tb.Rows[0]["userTypeName"]);
                MySession.Add("trueName", tb.Rows[0]["trueName"]);

                #endregion

                my.flag = 1;
                my.msg = "登录成功";
                return JsonConvert.SerializeObject(my);
            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "登录失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }

        }

        // 获取用户类型列表
        private string getTypeList()
        {
            pagingJson paging = new pagingJson();
            try
            {
                //获取Datatables发送的参数 必要
                int draw = Int32.Parse(Funcs.Get("draw"));//请求次数 这个值作者会直接返回给前台 

                //排序
                string orderColumn = Funcs.Get("order[0][column]");//那一列排序，从0开始
                string orderDir = Funcs.Get("order[0][dir]");//ase desc 升序或者降序

                //搜索
                string userType = GlobalObject.unescape(Funcs.Get("userType"));//搜索框值

                //分页
                int start = Int32.Parse(Funcs.Get("start"));//第一条数据的起始位置
                int length = Int32.Parse(Funcs.Get("length"));//每页显示条数

                // where条件,排序条件
                string strWhere = "parentId=1", strOrderBy = "";
                if (userType != null && userType != "")
                {
                    strWhere += " and paramsName like '%" + Funcs.ToSqlString(userType) + "%'";
                }
                if (orderColumn != "" && orderDir != "")
                {
                    strOrderBy = Funcs.Get("columns[" + orderColumn + "][data]") + " " + orderDir;
                }

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_table",SqlDbType.Text){ Value = "system_params" },
                    new SqlParameter("@param_field",SqlDbType.VarChar){ Value = "*" },
                    new SqlParameter("@param_where",SqlDbType.Text){ Value = strWhere },
                    new SqlParameter("@param_groupBy",SqlDbType.VarChar){ Value = "" },
                    new SqlParameter("@param_orderBy",SqlDbType.VarChar){ Value = strOrderBy },
                    new SqlParameter("@param_pageNumber",SqlDbType.VarChar){ Value = start/length+1 },
                    new SqlParameter("@param_pageSize",SqlDbType.Int){ Value = length },
                    new SqlParameter("@param_isCount",SqlDbType.Int){ Value = 1 }
                };

                DataSet ds = Utility.SqlHelper.ExecProcFillDataSet("sp_GetPagingList", param);
                paging.draw = draw;
                paging.recordsTotal = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
                paging.data = ds.Tables[0];
                paging.recordsFiltered = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
                return JsonConvert.SerializeObject(paging);
            }
            catch (Exception ex)
            {
                paging.error = "获取用户类型列表失败:" + ex.Message;
                paging.data = null;
                return JsonConvert.SerializeObject(paging);
            }
        }

        // 添加或编辑用户类型
        private string addOrEditUserType()
        {
            myJson my = new myJson();
            try
            {
                var id = Funcs.Get("id") == "" ? "0" : Funcs.Get("id"); //用户类型Id
                var paramsName = GlobalObject.unescape(Funcs.Get("paramsName")); //用户类型名称

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("param_id",SqlDbType.Int){ Value = id },
                    new SqlParameter("param_parentId",SqlDbType.Int){ Value = 1 },
                    new SqlParameter("param_paramsName",SqlDbType.VarChar){ Value = paramsName }
                };

                int count = Utility.SqlHelper.ExecProcNonQuery("sp_AddOrUpdateParam", param);

                if (count > 0)
                {
                    my.flag = 1;
                    my.msg = "保存成功！";
                    return JsonConvert.SerializeObject(my);
                }
                my.flag = 0;
                my.msg = "保存失败！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "保存失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 删除用户类型
        private string deleteUserType()
        {
            myJson json = new myJson();
            try
            {
                string id = Funcs.Get("id"); //用户类型id
                if (!Funcs.IsNumber(id))
                {
                    json.flag = 0;
                    json.msg = "参数错误，请刷新重试";
                    return JsonConvert.SerializeObject(json);
                }
                string strSql = "delete from system_params where id=" + id;
                int count = Utility.SqlHelper.ExecuteNonQuery(strSql);
                if (count > 0)
                {
                    json.flag = 1;
                    json.msg = "删除成功";
                    return JsonConvert.SerializeObject(json);
                }
                json.flag = 0;
                json.msg = "删除失败";
                return JsonConvert.SerializeObject(json);

            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "删除失败：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }

        // 获取用户列表
        private string getUserList()
        {
            pagingJson paging = new pagingJson();
            try
            {
                //获取Datatables发送的参数 必要
                int draw = Int32.Parse(Funcs.Get("draw"));//这个值作者会直接返回给前台

                //排序
                string orderColumn = Funcs.Get("order[0][column]");//那一列排序，从0开始
                string orderDir = Funcs.Get("order[0][dir]");//ase desc 升序或者降序

                //搜索
                string sStatus = Funcs.Get("sStatus"); //用户状态
                string sUserType = Funcs.Get("sUserType");//用户类型
                string sName = GlobalObject.unescape(Funcs.Get("sName"));//搜索框值

                //分页
                int start = Int32.Parse(Funcs.Get("start"));//第一条数据的起始位置
                int length = Int32.Parse(Funcs.Get("length"));//每页显示条数

                // where条件,排序条件
                string strWhere = "tt.userStatus!=2", strOrderBy = "";
                if (sStatus != null && sStatus != "")
                {
                    strWhere += " and userStatus=" + sStatus;
                }
                if (sUserType != null && sUserType != "")
                {
                    strWhere += " and userType=" + sUserType;
                }
                if (sName != null && sName != "")
                {
                    strWhere += " and (userName like '%" + Funcs.ToSqlString(sName) + "%' or trueName like '%" + Funcs.ToSqlString(sName) + "%')";
                }
                if (orderColumn != "" && orderDir != "")
                {
                    strOrderBy = Funcs.Get("columns[" + orderColumn + "][data]") + " " + orderDir;
                }

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_table",SqlDbType.Text){ Value = "(select su.*,sp.paramsName from system_users su left join system_params sp on su.userType=sp.id)tt" },
                    new SqlParameter("@param_field",SqlDbType.VarChar){ Value = "*" },
                    new SqlParameter("@param_where",SqlDbType.Text){ Value = strWhere },
                    new SqlParameter("@param_groupBy",SqlDbType.VarChar){ Value = "" },
                    new SqlParameter("@param_orderBy",SqlDbType.VarChar){ Value = strOrderBy },
                    new SqlParameter("@param_pageNumber",SqlDbType.VarChar){ Value = start/length+1 },
                    new SqlParameter("@param_pageSize",SqlDbType.Int){ Value = length },
                    new SqlParameter("@param_isCount",SqlDbType.Int){ Value = 1 }
                };

                DataSet ds = Utility.SqlHelper.ExecProcFillDataSet("sp_GetPagingList", param);
                paging.draw = draw;
                paging.recordsTotal = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
                paging.data = ds.Tables[0];
                paging.recordsFiltered = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
                return DateTimeFormat(paging, "yyyy-MM-dd HH:mm ");
            }
            catch (Exception ex)
            {
                paging.error = "获取用户列表失败:" + ex.Message;
                paging.data = null;
                return JsonConvert.SerializeObject(paging);
            }
        }

        // 获取用户信息
        private string getUserInfo()
        {
            myJson my = new myJson();
            try
            {
                var userId = Funcs.Get("userId");//用户Id

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@userId",SqlDbType.Int){ Value = userId }
                };

                string strSql = "select * from system_users where id=@userId";
                DataTable dt = Utility.SqlHelper.GetDataTable(strSql, param);
                if (dt == null || dt.Rows.Count < 1)
                {
                    my.flag = 0;
                    my.msg = "获取用户信息失败！";
                }
                my.flag = 1;
                my.obj = dt;
                my.msg = "获取用户信息成功！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "获取用户信息失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 新增或编辑用户
        private string addOrEditUser()
        {
            myJson my = new myJson();
            try
            {
                var userId = Funcs.Get("userId") == "" ? "0" : Funcs.Get("userId"); //用户Id
                var userName = GlobalObject.unescape(Funcs.Get("userName"));//用户名
                var nikeName = GlobalObject.unescape(Funcs.Get("nikeName"));//昵称
                var password = GlobalObject.unescape(Funcs.Get("password"));//密码
                var truename = GlobalObject.unescape(Funcs.Get("truename"));//真实姓名
                var userType = Funcs.Get("userType");//用户类型
                var userStatus = Funcs.Get("userStatus");//用户状态

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_userId",SqlDbType.Int){ Value = userId },
                    new SqlParameter("@param_userName",SqlDbType.VarChar){ Value = userName },
                    new SqlParameter("@param_nikeName",SqlDbType.VarChar){ Value = nikeName },
                    new SqlParameter("@param_pwd",SqlDbType.VarChar){ Value = Funcs.MD5(password) },
                    new SqlParameter("@param_trueName",SqlDbType.VarChar){ Value = truename },
                    new SqlParameter("@param_userType",SqlDbType.Int){ Value = userType },
                    new SqlParameter("@param_userStatus",SqlDbType.Int){ Value = userStatus },
                    new SqlParameter("@param_createTime",SqlDbType.DateTime){ Value = DateTime.Now }
                };
                //判断此用户名是否被使用
                string sql = "select count(*) from system_users where username=@param_userName and id!=@param_userId and userstatus!=2";
                int count = System.Convert.ToInt32(Utility.SqlHelper.ExecuteScalar(sql, param));
                if (count > 0)
                {
                    my.flag = 0;
                    my.msg = "此用户名已存在，请更换！";
                    return JsonConvert.SerializeObject(my);
                }

                count = Utility.SqlHelper.ExecProcNonQuery("sp_AddOrUpdateUser", param);

                if (count > 0)
                {
                    my.flag = 1;
                    my.msg = "保存成功！";
                    return JsonConvert.SerializeObject(my);
                }
                my.flag = 0;
                my.msg = "保存失败！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "保存失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 删除用户
        private string deleteUser()
        {
            myJson json = new myJson();
            try
            {
                string id = Funcs.Get("userId"); //用户类型id
                if (!Funcs.IsNumber(id))
                {
                    json.flag = 0;
                    json.msg = "参数错误，请刷新重试";
                    return JsonConvert.SerializeObject(json);
                }
                string strSql = "update system_users set userStatus=2 where id=" + id;
                int result = Utility.SqlHelper.ExecuteNonQuery(strSql);
                if (result > 0)
                {
                    json.flag = 1;
                    json.msg = "删除用户成功";
                    return JsonConvert.SerializeObject(json);
                }
                json.flag = 0;
                json.msg = "删除用户失败";
                return JsonConvert.SerializeObject(json);

            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "删除用户失败：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }

        // 修改密码
        private string changePwd()
        {
            myJson json = new myJson();
            try
            {
                string id = Funcs.Get("userId"); //用户id
                string password = GlobalObject.unescape(Funcs.Get("password"));//密码
                if (!Funcs.IsNumber(id))
                {
                    json.flag = 0;
                    json.msg = "参数错误，请刷新重试";
                    return JsonConvert.SerializeObject(json);
                }
                string strSql = "update system_users set password=@password where id=@id";

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@password",SqlDbType.VarChar){ Value = Funcs.MD5(password) },
                    new SqlParameter("@id",SqlDbType.Int){ Value = id }
                };

                int result = Utility.SqlHelper.ExecuteNonQuery(strSql, param);
                if (result > 0)
                {
                    json.flag = 1;
                    json.msg = "修改密码成功";
                    return JsonConvert.SerializeObject(json);
                }
                json.flag = 0;
                json.msg = "修改密码失败";
                return JsonConvert.SerializeObject(json);

            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "修改密码失败：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }

        // 获得用户权限
        private string getUserPower()
        {
            myJson my = new myJson();
            try
            {
                var userId = Funcs.Get("userId");//用户Id

                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("userId", SqlDbType.Int);
                parameters[0].Value = userId;

                string strSql = "select * from system_menu";
                DataTable dt = Utility.SqlHelper.GetDataTable(strSql);
                if (dt == null || dt.Rows.Count < 1)
                {
                    my.flag = 0;
                    my.msg = "获取菜单列表失败！";
                }
                my.obj = dt;
                strSql = @"select sm.id,sm.parentId from system_menu sm 
                           left join system_power sp on sm.id=sp.menuId 
                           where sp.userId=@userId order by sortValue";
                dt = Utility.SqlHelper.GetDataTable(strSql, parameters);
                if (dt == null)
                {
                    my.flag = 0;
                    my.msg = "获取当前用户权限失败！";
                }
                my.obj2 = dt;
                my.flag = 1;
                my.msg = "获取用户权限成功！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "获取用户权限失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 保存权限
        private string savePowers()
        {
            myJson my = new myJson();
            try
            {
                string userid = Funcs.Get("userid");
                string idlist = Funcs.Get("ids").TrimEnd(',');
                if (!Funcs.IsNumber(userid) || idlist.Length < 1)
                {
                    my.flag = 0;
                    my.msg = "参数错误，请刷新重试";
                    return JsonConvert.SerializeObject(my);
                }
               
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@param_userid", SqlDbType.Int);
                parameters[1] = new SqlParameter("@param_addids", SqlDbType.VarChar);
                parameters[0].Value = userid;
                parameters[1].Value = idlist.ToString();

                int count = Utility.SqlHelper.ExecProcNonQuery("sp_AddOrUpdatePower", parameters);
                if (count > 0)
                {
                    my.flag = 1;
                    my.msg = "保存成功";
                    return JsonConvert.SerializeObject(my);
                }
                my.flag = 0;
                my.msg = "保存失败";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "保存失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 获取菜单树
        //private string getMenuTree()
        //{
        //    myJson json = new myJson();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        DataRow[] clildren = null;
        //        //获取省份
        //        string strSql = "select * from adop.areacode where province=city order by areacode";
        //        DataTable tbP = Utility.SqlHelper.GetDataTable(strSql);
        //        //获取市
        //        strSql = "select * from adop.areacode where province!=city order by areacode";
        //        DataTable tbC = Utility.SqlHelper.GetDataTable(strSql);

        //        sb.Append("[");
        //        foreach (DataRow pRow in tbP.Rows)
        //        {
        //            clildren = tbC.Select(string.Format("province='{0}'", pRow["province"].ToString()));
        //            if (clildren.Length == 0)
        //            {
        //                sb.Append("{\"id\":\"" + pRow["AREACODE"].ToString() + "\",\"name\":\"" + pRow["CITY"].ToString() + "\"},");
        //                //sb.Append("{\"id\":\"" + pRow["AREACODE"].ToString() + "\",\"name\":\"" + pRow["province"].ToString() + "\",\"children\":[");
        //                //sb.Append("{\"id\":\"" + pRow["AREACODE"].ToString() + "\",\"name\":\"" + pRow["CITY"].ToString() + "\"},");
        //                //sb.Append("]},");
        //            }
        //            else
        //            {
        //                sb.Append("{\"id\":\"" + pRow["AREACODE"].ToString() + "\",\"name\":\"" + pRow["province"].ToString() + "\",\"children\":[");

        //                foreach (DataRow cRow in clildren)
        //                {
        //                    sb.Append("{\"id\":\"" + cRow["AREACODE"].ToString() + "\",\"name\":\"" + cRow["CITY"].ToString() + "\"},");
        //                }
        //                sb.Remove(sb.Length - 1, 1);
        //                sb.Append("]},");
        //            }
        //        }
        //        sb.Remove(sb.Length - 1, 1);
        //        sb.Append("]");
        //        json.flag = 1;
        //        json.obj = sb;
        //        return JsonConvert.SerializeObject(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        json.flag = 0;
        //        json.msg = "加载中国地区树失败：" + ex.Message;
        //        return JsonConvert.SerializeObject(json);
        //    }
        //}


        DataTable dtMenu = null;
        DataRow[] menuChildren = null;
        StringBuilder strbMenu = new StringBuilder();
        //绑定菜单
        private string getMenuTree()
        {
            myJson json = new myJson();
            try
            {
                string strSql = "select * from system_menu order by parentId,sortvalue";
                dtMenu = Utility.SqlHelper.GetDataTable(strSql);
                strbMenu.Append("[");
                MenuBuild("0");
                strbMenu.Append("]");
                json.flag = 1;
                json.obj = strbMenu.ToString();
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "菜单绑定错误：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }

        //构建菜单
        public void MenuBuild(string menuId)
        {
            DataRow[] List = dtMenu.Select(string.Format("parentId={0}", menuId));
            for (int i = 0; i < List.Length; i++)
            {
                menuChildren = dtMenu.Select(string.Format("parentId={0}", List[i]["id"].ToString()));
                if (menuChildren.Length != 0)
                {
                    strbMenu.Append("{\"id\":\"" + List[i]["id"].ToString() + "\",\"name\":\"" + List[i]["menuName"].ToString() + "\",\"children\":[");
                    MenuBuild(List[i]["id"].ToString());
                    strbMenu.Append("]},");
                }
                else
                {
                    strbMenu.Append("{\"id\":\"" + List[i]["id"].ToString() + "\",\"name\":\"" + List[i]["menuName"].ToString() + "\"},");
                }
                if (List.Length - 1 == i)
                {
                    strbMenu.Remove(strbMenu.Length - 1, 1);
                }
            }
        }

        //获取用户菜单
        private string getUserMenu()
        {
            myJson json = new myJson();
            try
            {
                var userId = Funcs.Get("userId");//用户Id
                string strSql = "select * from system_power where userId=@userId";
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@userId",SqlDbType.Int){ Value = userId },
                };
                DataTable dt = Utility.SqlHelper.GetDataTable(strSql, param);
                json.flag = 1;
                json.obj = dt;
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "获取用户菜单失败：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }

    }
}