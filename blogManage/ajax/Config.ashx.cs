using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blogManage.baseClass;
using Utility;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Microsoft.JScript;
using System.Data.SqlClient;

namespace blogManage.ajax
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class Config : BaseAshx
    {

        public override void ProcessRequest(HttpContext context)
        {
            action = Funcs.Get("action");
            //判断是否未登录
            if (!islogin())
            {
                returnData = returnLogin();
            }
            else
            {
                OperateAciton oa = new OperateAciton();

                oa.GatherOperate("getBlogMenuTree", getBlogMenuTree);               // 绑定博客导航
                oa.GatherOperate("addOrEditBlogMenu", addOrEditBlogMenu);           // 新增或编辑博客导航
                oa.GatherOperate("getBlogMenuInfo", getBlogMenuInfo);               // 获取博客导航信息
                oa.GatherOperate("deleteBlogMenu", deleteBlogMenu);                 // 删除博客导航
                
                returnData = oa.ExecuteOperate(action);
            }
            context.Response.Write(returnData);
        }

        DataTable dtMenu = null;
        DataRow[] menuChildren = null;
        StringBuilder strbMenu = new StringBuilder();
        // 绑定博客菜单
        private string getBlogMenuTree()
        {
            myJson json = new myJson();
            try
            {
                string strSql = "select * from tb_blogmenu where status=1 order by parentId,sort";
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

        // 构建导航
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

        // 新增或编辑博客导航
        private string addOrEditBlogMenu()
        {
            myJson my = new myJson();
            try
            {
                var blogMenuId = Funcs.Get("blogMenuId") == "" ? "0" : Funcs.Get("blogMenuId"); //博客菜单Id
                var menuName = GlobalObject.unescape(Funcs.Get("menuName")); //菜单名称
                var menuUrl = GlobalObject.unescape(Funcs.Get("menuUrl")); //菜单Url
                var menuKey = GlobalObject.unescape(Funcs.Get("menuKey")); //菜单Key   
                var category = Funcs.Get("category"); //文章类型           
                var parentId = Funcs.Get("parentId"); //父菜单
                var showType = Funcs.Get("showType"); //列表显示类型
                var sort = Funcs.Get("sort"); //排序


                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("param_blogMenuId",SqlDbType.Int){ Value = blogMenuId },
                    new SqlParameter("param_menuName",SqlDbType.VarChar){ Value = menuName },
                    new SqlParameter("param_menuUrl",SqlDbType.VarChar){ Value = menuUrl },
                    new SqlParameter("param_menuKey",SqlDbType.VarChar){ Value = menuKey },
                    new SqlParameter("param_category",SqlDbType.VarChar){ Value = category },
                    new SqlParameter("param_parentId",SqlDbType.Int){ Value = parentId },
                    new SqlParameter("param_showType",SqlDbType.Int){ Value = showType },
                    new SqlParameter("param_sort",SqlDbType.Int){ Value = sort }
                };

                int count = Utility.SqlHelper.ExecProcNonQuery("sp_AddOrEditBlogMenu", param);

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

        // 获取博客导航信息
        private string getBlogMenuInfo()
        {
            myJson my = new myJson();
            try
            {
                var blogMenuId = Funcs.Get("blogMenuId");//导航Id

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@blogMenuId",SqlDbType.Int){ Value = blogMenuId }
                };

                string strSql = "select * from tb_blogMenu where id=@blogMenuId";
                DataTable dt = Utility.SqlHelper.GetDataTable(strSql, param);
                if (dt == null || dt.Rows.Count < 1)
                {
                    my.flag = 0;
                    my.msg = "获取导航信息失败！";
                    return JsonConvert.SerializeObject(my);
                }
                my.flag = 1;
                my.obj = dt;
                my.msg = "获取导航信息成功！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "获取导航信息失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 删除博客导航
        private string deleteBlogMenu()
        {
            myJson json = new myJson();
            try
            {
                string id = Funcs.Get("bolgMenuId"); //博客导航Id
                if (!Funcs.IsNumber(id))
                {
                    json.flag = 0;
                    json.msg = "参数错误，请刷新重试";
                    return JsonConvert.SerializeObject(json);
                }
                string strSql = "update tb_blogMenu set status=0 where id=" + id;
                int result = Utility.SqlHelper.ExecuteNonQuery(strSql);
                if (result > 0)
                {
                    json.flag = 1;
                    json.msg = "删除导航成功";
                    return JsonConvert.SerializeObject(json);
                }
                json.flag = 0;
                json.msg = "删除导航失败";
                return JsonConvert.SerializeObject(json);

            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "删除导航失败：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }
    }
}