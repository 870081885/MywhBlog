using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mywhBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //BundleTable.EnableOptimizations = false;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var strFilePath = Context.Request.FilePath;
            var strLastSlash = strFilePath.Substring(strFilePath.LastIndexOf("/"));
            if (strLastSlash.Contains(".")|| strLastSlash=="/")
            {
                return;
            }
            //var lastChar = strFilePath.Substring(strFilePath.Length - 1, 1);
            //if (strFilePath.Length == 1 && lastChar == "/")
            //{
            //    return;
            //}
            //else if (strFilePath.Contains(".html") || lastChar == "/")
            //{
            //    return;
            //}

            Context.Response.Redirect(strFilePath + "/");

        }
    }
}
