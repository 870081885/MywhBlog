using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace blogManage.assets.plugins.umeditor.net
{
    /// <summary>
    /// imageUp1 的摘要说明
    /// </summary>
    public class imageUp1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //上传配置
            
            string pathbase = System.Configuration.ConfigurationManager.AppSettings["uploadPath"].ToString()+"articleImg/";     //保存路径
            int size = 10;                                                                                                      //文件大小限制,单位mb                                                           
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                                                    //文件允许格式

            string callback = context.Request["callback"];
            string editorId = context.Request["editorid"];

            //上传图片
            Hashtable info;
            Uploader up = new Uploader();
            info = up.upFile(context, pathbase, filetype, size); //获取上传状态
            string json = BuildJson(info);

            context.Response.ContentType = "text/html";
            if (callback != null)
            {
                context.Response.Write(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json));
            }
            else
            {
                context.Response.Write(json);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] { "originalName", "name", "url", "size", "state", "type" };
            for (int i = 0; i < keys.Length; i++)
            {
                fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }
            return "{" + String.Join(",", fields) + "}";
        }
    }
}