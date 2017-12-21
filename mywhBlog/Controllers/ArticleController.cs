using mywhBlog.Business;
using mywhBlog.Entity;
using mywhBlog.Models.Article;
using mywhBlog.Models.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace mywhBlog.Controllers
{
    public class ArticleController : Controller
    {
        //首页
        public ActionResult Index(int page=1)
        {
            ArticleDAL dal = new ArticleDAL();
            DataSet ds=dal.getLatelyArticleList(page);
            Paging<Article> model = new Paging<Article>();
            model.list = ConvertHelper.FillModelList<Article>(ds.Tables[0]);
            model.totalPages = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            ViewBag.page = page;
            return View(model);
        }

        //文章列表
        public ActionResult List(string keyword1="",string keyword2="",int page=1)
        {
            string menuKey = "";
            if (keyword2 == "")
            {
                menuKey = keyword1;
            }
            else
            {
                menuKey = keyword1 + "/" + keyword2;
            }
            MyResult result = new MyResult();
            int count = Convert.ToInt32(ConfigurationManager.AppSettings["articleListCount"]);
            ArticleDAL dal = new ArticleDAL();
            result = dal.getArticleList(menuKey, count, page);
            ArticleList<Article> model = new ArticleList<Article>();
            model.head = ConvertHelper.FillModel<Head>(((DataTable)result.obj).Rows[0]);
            Paging<Article> paging = new Paging<Article>();
            paging.list= ConvertHelper.FillModelList<Article>((DataTable)result.obj2);
            paging.totalPages = Convert.ToInt32(result.obj3);
            model.paging = paging;
            ViewBag.page = page;
            ViewBag.menuKey = menuKey;
            return View(model);
        }

        //文章内容
        public ActionResult Content(string keyword1 = "", string keyword2 = "", int articleId = 1)
        {
            string menuKey = "";
            if (keyword2 == "")
            {
                menuKey = keyword1;
            }
            else
            {
                menuKey = keyword1 + "/" + keyword2;
            }
            MyResult result = new MyResult();
            ArticleDAL dal = new ArticleDAL();
            result = dal.getArticleContent(menuKey,articleId);
            ArticleContent model = new ArticleContent();
            //Article article = new Article();
            //ArticleTag articleTag = new ArticleTag();
            //PrevNext prevNext = new PrevNext();
            model.article = ConvertHelper.FillModel<Article>(((DataTable)result.obj).Rows[0]); 
            model.articleTag = ConvertHelper.FillModelList<ArticleTag>(((DataTable)result.obj2));
            model.prevNext = ConvertHelper.FillModelList<PrevNext>(((DataTable)result.obj3));
            ViewBag.menuKey = menuKey;
            return View(model);
        }

        //标签列表
        public ActionResult HotTagList()
        {
            ArticleDAL dal = new ArticleDAL();
            DataTable dt = dal.getHotTagList();
            StringBuilder strB = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                strB.Append("<li><a href=\"javascript:void(0);\" title=\"" + row["tagName"].ToString() + "\">" + row["tagName"].ToString() + "</a></li>");
            }
            ViewBag.strHotTag = strB.ToString();
            return PartialView();
        }

        //搜索
        public ActionResult Search()
        {
            return View();
        }
    }
}
