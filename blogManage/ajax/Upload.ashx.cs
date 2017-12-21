using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blogManage.baseClass;
using Utility;
using Newtonsoft.Json;
using System.IO;
using Utility.Utility;

namespace blogManage.ajax
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : BaseAshx
    {

        private string imgfilePath = "/upload/adImg";
        private string date = "";

        public override void ProcessRequest(HttpContext context)
        {
            date = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString();
            action = Funcs.Get("action");
            if (!islogin())
            {
                returnData = returnLogin();
            }
            else
            {
                OperateAciton oa = new OperateAciton();
                oa.GatherOperate("uploadImg", uploadImg);                               // 上传img          

                returnData = oa.ExecuteOperate(action);
            }
            context.Response.Write(returnData);
        }

        // 上传img
        private string uploadImg()
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile file = context.Request.Files[0];
            myJson json = new myJson();
            try
            {
                if (file.ContentLength <= 0 || string.IsNullOrEmpty(file.FileName))
                {
                    json.flag = 0;
                    json.msg = "上传错误，没有选择文件";
                    return JsonConvert.SerializeObject(json);
                }

                string Extension = Path.GetExtension(file.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".bmp")
                {

                    byte[] buffer = new byte[file.InputStream.Length];
                    file.InputStream.Read(buffer, 0, buffer.Length);
                    string name = ImageLibrary.md5(buffer);
                    string path = imgfilePath + "/" + date + "/";
                    string absolutePath = context.Server.MapPath(path);
                    if (!Directory.Exists(absolutePath))
                        Directory.CreateDirectory(absolutePath);
                    file.SaveAs(absolutePath + name + Extension);
                    json.flag = 1;
                    json.obj = path + name + Extension;
                    return JsonConvert.SerializeObject(json);
                }
                else
                {
                    json.flag = 0;
                    json.msg = "类型错误，只能上传图片";
                    return JsonConvert.SerializeObject(json);
                }
            }
            catch (Exception ex)
            {
                json.flag = 0;
                json.msg = "上传失败：" + ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }


    }
}