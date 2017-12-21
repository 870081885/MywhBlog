using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using blogManage;

namespace blogManage.baseClass
{
    /// <summary>
    /// BaseAshx 的摘要说明
    /// </summary>
    public class BaseAshx : IHttpHandler, IRequiresSessionState
    {
        public string action = "";      //方法名
        public string returnData = "";  //返回值

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
        }

        #region 是否登录
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        public bool islogin()
        {
            if (MySession.GetSessionStringValue("userName") == "")
            {
                return false;
            }
            else if (Global.IsOut(HttpContext.Current.Session.SessionID))
            {
                MySession.Clear();
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 返回登录
        /// <summary>
        /// 返回登录
        /// </summary>
        /// <returns></returns>
        public string returnLogin()
        {
            myJson my = new myJson();
            if (MySession.GetSessionStringValue("userName") == "")
            {
                my.flag = -100;
                my.msg = "无权访问！";
            }
            else
            {
                my.flag = -101;
                my.msg = "您的帐号在其他地方登录，被迫下线！";
            }
            return JsonConvert.SerializeObject(my);
        }
        #endregion

        #region 日期格式化
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="json">json</param>
        /// <param name="fromat">格式</param>
        /// <returns></returns>
        public string DateTimeFormat(pagingJson json,string fromat)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = fromat;
            return JsonConvert.SerializeObject(json, Formatting.Indented, timeFormat);
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}