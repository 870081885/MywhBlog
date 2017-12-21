using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mywhBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //首页（0个）
            routes.MapRoute(
                name: "Index",
                url: "",
                defaults: new { controller = "Article", action = "Index" }
            );


            //首页分页（1个）
            routes.MapRoute(
                name: "IndexPage",
                url: "index_{page}.html",
                defaults: new { controller = "Article", action = "Index" }
            );

            //关于博主（1个）
            routes.MapRoute(
                name: "AboutUs",
                url: "about",
                defaults: new { controller = "About", action = "Index" }
            );

            //404状态码
            routes.MapRoute(
                name: "404",
                url: "404",
                defaults: new { controller = "Others", action = "Index" }
            );

            //搜索（1个）
            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Article", action = "Search" }
            );

            //文章类型（1个）
            routes.MapRoute(
                name: "Category",
                url: "{keyword1}",
                defaults: new { controller = "Article", action = "List" }
            );

            //文章类型分页（2个）
            routes.MapRoute(
               name: "CategoryPage",
               url: "{keyword1}/list_{page}.html",
               defaults: new { controller = "Article", action = "List" },
               constraints: new { page = @"\d+" }
            );

            //文章内容映射(2个)
            routes.MapRoute(
                name: "Article",
                url: "{keyword1}/{articleId}.html",
                defaults: new { controller = "Article", action = "Content" },
                constraints: new { articleId = @"\d+" }
            );
            //菜单(2个)
            routes.MapRoute(
                name: "BindMenu",
                url: "Menu/BindMenu",
                defaults: new { controller = "Menu", action = "BindMenu" }
            );


            //test
            routes.MapRoute(
                name: "test",
                url: "Article/Test",
                defaults: new { controller = "Article", action = "Test" }
            );

            //热门标签(2个)
            routes.MapRoute(
                name: "HotTagList",
                url: "Article/HotTagList",
                defaults: new { controller = "Article", action = "HotTagList" }
            );

            //文章类型（2个）
            routes.MapRoute(
                name: "Category2",
                url: "{keyword1}/{keyword2}",
                defaults: new { controller = "Article", action = "List" }
            );

            //文章类型2分页（3个）
            routes.MapRoute(
               name: "Category2Page",
               url: "{keyword1}/{keyword2}/list_{page}.html",
               defaults: new { controller = "Article", action = "List" },
               constraints: new { page = @"\d+" }
            );

            //文章内容映射2（3个）
            routes.MapRoute(
                name: "Article2",
                url: "{keyword1}/{keyword2}/{articleId}.html",
                defaults: new { controller = "Article", action = "Content" },
                constraints: new { articleId = @"\d+" }
            );

        }
    }
}
