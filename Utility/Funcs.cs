using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;

namespace Utility
{
    public static class Funcs
    {
        #region 得到页面传递过来的参数
        /// <summary>
        /// 得到页面传递过来的参数
        /// </summary>
        /// <param name="s">参数</param>
        /// <returns></returns>
        public static string Get(string s)
        {
            if (System.Web.HttpContext.Current.Request[s] == null) return "";
            else return System.Web.HttpContext.Current.Request[s].ToString().Trim();
        }
        #endregion

        #region 将字符串MD5加密
        /// <summary>
        /// 将字符串MD5加密
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string MD5(string data)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(data, "md5");
        }
        #endregion

        #region 验证字符串是否为空
        /// <summary>
        /// 验证字符串是否为空
        /// </summary>
        /// <param name="text">需要验证的字符串</param>
        /// <returns></returns>
        public static Boolean IsBlank(string text)
        {
            if (text == null) return true;
            text = text.Trim();
            if (text == "") return true;
            return false;
        }
        #endregion

        #region SQL参数编码
        /// <summary>
        /// 将拼接的SQL参数进行编码
        /// </summary>
        /// <param name="s">需要编码的参数</param>
        /// <returns></returns>
        public static string ToSqlString(string s)
        {
            return (IsBlank(s)) ? "" : s.Replace("'", "''").Replace("•", "&#8226;").Trim();
        }
        /// <summary>
        /// 将拼接SQL时使用LIKE的参数进行编码
        /// </summary>
        /// <param name="s">需要编码的参数</param>
        /// <returns></returns>
        public static string ToLikeString(string s)
        {
            s = ToSqlString(s);
            s = s.Replace("[", "[[]");
            s = s.Replace("]", "[]]");
            s = s.Replace("%", "[%]");
            s = s.Replace("_", "[_]");
            s = s.Replace("^", "[^]");
            s = s.Replace("#", "[#]");
            return s;
        }
        #endregion

        #region 检验是否为数字
        public const string NumberRule = @"^[+-]?[0123456789]+[.]?[0123456789]*$";
        /// <summary>
        /// 检验是否为数字
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为数字：true代表是，false代表否</returns>
        public static bool IsNumber(string str)
        {
            return Regex.IsMatch(str.Trim(), NumberRule);
        }
        #endregion

        #region 日期格式化
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="json">json</param>
        /// <param name="fromat">格式</param>
        /// <returns></returns>
        public static string DateTimeFormat(myJson json, string fromat)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = fromat;
            return JsonConvert.SerializeObject(json, Formatting.Indented, timeFormat);
        }
        #endregion
    }
}
