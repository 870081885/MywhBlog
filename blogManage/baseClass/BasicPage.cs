using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;

namespace blogManage.baseClass
{
    public class BasicPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            //判断是否登录
            string userid = MySession.GetSessionStringValue("UserId");
            if (userid == "")
            {
                MySession.Clear();
                //var path = Request.Url.LocalPath;
                var vMsg = "您的登录已过期，请重新登录";
                HttpContext.Current.Response.Write("<script>alert(\"" + vMsg + "\");window.top.location.href='/login.aspx';</script>");
                return;
            }


            base.OnInit(e);
        }
    }
}