using mywhBlog.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;

namespace mywhBlog.ajax
{
    /// <summary>
    /// Article 的摘要说明
    /// </summary>
    public class Article : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = Funcs.Get("action");
            OperateAciton oa = new OperateAciton();

            oa.GatherOperate("clickArticle", clickArticle);                 // 访问文章

            string returnData = oa.ExecuteOperate(action);
            context.Response.Write(returnData);
        }

        // 访问文章
        private string clickArticle()
        {
            myJson my = new myJson();
            try
            {
                var articleId = Funcs.Get("articleId");     //文章id
                ArticleDAL dal = new ArticleDAL();
                dal.clickArticle(articleId, HttpContext.Current.Request.UserHostAddress);
                my.flag = 1;
                my.msg = "访问文章成功！";
                return JsonConvert.SerializeObject(my);

            }
            catch (Exception ex)
            {
                my.flag = 0;
                my.msg = "访问文章失败：" + ex.Message;
                return JsonConvert.SerializeObject(my);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}