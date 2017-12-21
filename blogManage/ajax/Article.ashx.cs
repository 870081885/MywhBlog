using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blogManage.baseClass;
using Utility;
using Microsoft.JScript;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace blogManage.ajax
{
    /// <summary>
    /// Article1 的摘要说明
    /// </summary>
    public class Article1 : BaseAshx
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
                oa.GatherOperate("getArticleList", getArticleList);                 // 获取文章列表
                oa.GatherOperate("addOrEditArticle", addOrEditArticle);             // 新增或编辑文章
                oa.GatherOperate("getArticleInfo", getArticleInfo);                 // 新增或编辑文章
                oa.GatherOperate("deleteArticle", deleteArticle);                   // 删除文章
                oa.GatherOperate("getCategoryList", getCategoryList);               // 获取文章类型列表
                oa.GatherOperate("addOrEditCategory", addOrEditCategory);           // 新增或编辑文章类型
                oa.GatherOperate("deleteCategory", deleteCategory);                 // 删除文章类型
                oa.GatherOperate("getCategoryInfo", getCategoryInfo);               // 获取文章类型信息

                returnData = oa.ExecuteOperate(action);
            }
            context.Response.Write(returnData);
        }

        // 获取文章列表
        private string getArticleList()
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
                string sCategory = Funcs.Get("sCategory");//文章类型
                string sTitle = GlobalObject.unescape(Funcs.Get("sTitle"));//标题

                //分页
                int start = Int32.Parse(Funcs.Get("start"));//第一条数据的起始位置
                int length = Int32.Parse(Funcs.Get("length"));//每页显示条数

                // where条件,排序条件
                string strWhere = "status!=2", strOrderBy = "";
                if (sCategory != null && sCategory != "")
                {
                    strWhere += " and categoryId =" + sCategory;
                }
                if (sTitle != null && sTitle != "")
                {
                    strWhere += " and title like '%" + Funcs.ToSqlString(sTitle) + "%'";
                }
                if (orderColumn != "" && orderDir != "")
                {
                    strOrderBy = Funcs.Get("columns[" + orderColumn + "][data]") + " " + orderDir;
                }
                string strTB = "(select a.*,c.categoryName from tb_article a left join tb_category c on a.categoryId=c.categoryId)tt";

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_table",SqlDbType.Text){ Value = strTB },
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
                return DateTimeFormat(paging,"yyyy-MM-dd hh:mm");
            }
            catch (Exception ex)
            {
                paging.error = "获取文章列表失败:" + ex.Message;
                paging.data = null;
                return JsonConvert.SerializeObject(paging);
            }
        }

        // 新增或编辑文章
        private string addOrEditArticle()
        {
            myJson my = new myJson();
            try
            {
                var articleId = Funcs.Get("articleId") == "" ? "0" : Funcs.Get("articleId"); //文章Id
                var title = GlobalObject.unescape(Funcs.Get("title"));//标题
                var category = Funcs.Get("category");//文章类型
                var tags = GlobalObject.unescape(Funcs.Get("tags"));//标签
                var isTop = Funcs.Get("isTop");//是否置顶
                var img = Funcs.Get("img");//图片     
                var content = GlobalObject.unescape(Funcs.Get("content"));//文章内容

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_articleId",SqlDbType.Int){ Value = articleId },
                    new SqlParameter("@param_title",SqlDbType.VarChar){ Value = title },
                    new SqlParameter("@param_category",SqlDbType.VarChar){ Value = category },
                    new SqlParameter("@param_tags",SqlDbType.VarChar){ Value = tags },
                    new SqlParameter("@param_isTop",SqlDbType.Int){ Value = isTop },
                    new SqlParameter("@param_img",SqlDbType.VarChar){ Value = img },
                    new SqlParameter("@param_content",SqlDbType.Text){ Value = content },
                    new SqlParameter("@param_userId",SqlDbType.Int){ Value = MySession.GetUserID() }
                };

                Utility.SqlHelper.ExecProcNonQuery("sp_AddOrEditArticle", param);

                my.flag = 1;
                my.msg = "保存成功！";
                return JsonConvert.SerializeObject(my);
            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "保存失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 获取文章信息
        private string getArticleInfo()
        {
            myJson my = new myJson();
            try
            {
                var articleId = Funcs.Get("articleId");//文章Id

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@articleId",SqlDbType.Int){ Value = articleId }
                };

                string strSql = "select * from tb_article where id=@articleId";
                DataTable dt = Utility.SqlHelper.GetDataTable(strSql, param);
                if (dt == null || dt.Rows.Count < 1)
                {
                    my.flag = 0;
                    my.msg = "获取导航信息失败！";
                    return JsonConvert.SerializeObject(my);
                }
                strSql = "select * from tb_article_tag where articleId=@articleId";
                DataTable dt2 = Utility.SqlHelper.GetDataTable(strSql, param);
                my.flag = 1;
                my.obj = dt;
                my.obj2 = dt2;
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

        // 删除文章
        private string deleteArticle()
        {
            myJson json = new myJson();
            try
            {
                string articleId = Funcs.Get("articleId"); //文章id
                if (!Funcs.IsNumber(articleId))
                {
                    json.flag = 0;
                    json.msg = "参数错误，请刷新重试";
                    return JsonConvert.SerializeObject(json);
                }
                string strSql = "update tb_article set status=2 where id=" + articleId;
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

        // 获取文章类型列表
        private string getCategoryList()
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
                string sCategory = GlobalObject.unescape(Funcs.Get("sCategory"));//文章类型名称或id

                //分页
                int start = Int32.Parse(Funcs.Get("start"));//第一条数据的起始位置
                int length = Int32.Parse(Funcs.Get("length"));//每页显示条数

                // where条件,排序条件
                string strWhere = "1=1", strOrderBy = "";
                if (sCategory != null && sCategory != "")
                {
                    strWhere += " and (categoryId like '%" + Funcs.ToSqlString(sCategory) + "%' or categoryName like '%" + Funcs.ToSqlString(sCategory) + "%')";
                }
                if (orderColumn != "" && orderDir != "")
                {
                    strOrderBy = Funcs.Get("columns[" + orderColumn + "][data]") + " " + orderDir;
                }
                string strTB = "(select top 100 percent * from tb_category order by parentId)tt";

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_table",SqlDbType.Text){ Value = strTB },
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
                paging.error = "获取文章类型列表失败:" + ex.Message;
                paging.data = null;
                return JsonConvert.SerializeObject(paging);
            }
        }

        // 新增或编辑文章类型
        private string addOrEditCategory()
        {
            myJson my = new myJson();
            try
            {
                var oldCategoryId = Funcs.Get("oldCategoryId"); //文章类型旧Id
                var newCategoryId = Funcs.Get("newCategoryId");//文章类型新Id
                var categoryName = GlobalObject.unescape(Funcs.Get("categoryName"));//文章类型名称
                var parentId = Funcs.Get("parentId");//父类型

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@param_oldCategoryId",SqlDbType.VarChar){ Value = oldCategoryId },
                    new SqlParameter("@param_newCategoryId",SqlDbType.VarChar){ Value = newCategoryId },
                    new SqlParameter("@param_categoryName",SqlDbType.VarChar){ Value = categoryName },
                    new SqlParameter("@param_parentId",SqlDbType.VarChar){ Value = parentId },
                    new SqlParameter("@param_retCode",SqlDbType.Int){ Direction=ParameterDirection.Output},
                    new SqlParameter("@param_retMsg",SqlDbType.VarChar){ Direction=ParameterDirection.Output,Size=50}

                };

                Utility.SqlHelper.ExecProcNonQuery("sp_AddOrEditCategory", param);

                my.flag = System.Convert.ToInt32(param[4].Value);
                my.msg = param[5].Value.ToString();
                return JsonConvert.SerializeObject(my);
            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "保存失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        // 删除文章类型
        private string deleteCategory()
        {
            myJson json = new myJson();
            try
            {
                string categoryId = Funcs.Get("categoryId"); //文章类型id
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@categoryId",SqlDbType.VarChar){ Value = categoryId }
                };
                string strSql = "delete from tb_category where categoryId=@categoryId";
                int count = Utility.SqlHelper.ExecuteNonQuery(strSql,param);
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

        // 获取文章类型信息
        private string getCategoryInfo()
        {
            myJson my = new myJson();
            try
            {
                var categoryId = Funcs.Get("categoryId");//文章类型Id

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@categoryId",SqlDbType.VarChar){ Value = categoryId }
                };

                string strSql = "select * from tb_category where categoryId=@categoryId";
                DataTable dt = Utility.SqlHelper.GetDataTable(strSql, param);
                if (dt == null || dt.Rows.Count < 1)
                {
                    my.flag = 0;
                    my.msg = "获取文章类型信息失败！";
                }
                my.flag = 1;
                my.obj = dt;
                my.msg = "获取文章类型信息成功！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "获取文章类型信息失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }
    }
}