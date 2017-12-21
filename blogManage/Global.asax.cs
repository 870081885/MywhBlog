using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace blogManage
{
    public class Global : System.Web.HttpApplication
    {
        public static Dictionary<string, int> _mDic = new Dictionary<string, int>();
        //public static string GetVersion = "20150827";

        #region 添加登录用户
        /// <summary>
        /// 添加登录用户
        /// </summary>
        /// <param name="vUserid"></param>
        /// <param name="vSessionID"></param>
        public static void Add(int vUserid, string vSessionID)
        {
            foreach (KeyValuePair<string, int> kvp in _mDic)
            {
                if (kvp.Value == vUserid)
                {
                    _mDic.Remove(kvp.Key);
                    break;
                }
            }

            if (_mDic.ContainsKey(vSessionID))
            {
                _mDic.Remove(vSessionID);
            }
            _mDic.Add(vSessionID, vUserid);
        }
        #endregion

        #region 退出
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="vSessionID"></param>
        /// <returns></returns>
        public static bool IsOut(string vSessionID)
        {
            return !_mDic.ContainsKey(vSessionID);
        }
        #endregion

        protected void Application_Start(object sender, EventArgs e)
        {
        }
    }
}