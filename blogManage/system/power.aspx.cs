using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using blogManage.baseClass;
using System.Text;
using System.Data;
using Utility;
using Microsoft.JScript;

namespace blogManage.system
{
    public partial class power : BasicPage
    {
        public static string menulist = "", userid = "", userName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userName = GlobalObject.unescape(Funcs.Get("userName"));
                userid = Funcs.Get("userId");
                //getmenulist();
            }
        }

        private void getmenulist()
        {
            string strSql = "select id,menuname,parentid from system_menu order by sortvalue desc ";
            DataTable dt = SqlHelper.GetDataTable(strSql);
            if (dt == null || dt.Rows.Count < 1)
            {
                menulist = "<tr><td colspan='2'>暂无菜单</td></tr>";
                return;
            }
            //用户权限
            List<string> powerList = userpower();

            StringBuilder sbr = new StringBuilder();
            DataRow[] premenu = dt.Select("parentid=0");
            foreach (DataRow item in premenu)
            {
                sbr.Append("<tr>");
                sbr.Append("<td>" + item["menuname"] + "</td>");
                sbr.Append("<td>");
                DataRow[] submenu = dt.Select("parentid=" + item["id"]);
                int i = 0;
                string isck = "";
                foreach (DataRow subitem in submenu)
                {
                    isck = "";
                    if (powerList != null)
                    {
                        isck = powerList.Contains(subitem["id"].ToString()) ? "checked='checked'" : "";
                    }
                    sbr.Append("<span style=\"margin-right:10px;\"><input type=\"checkbox\" name=\"check2\" value=\"" + subitem["id"] + "\"" + isck + "/>" + subitem["menuname"] + "</span>");
                    i++;
                }
                sbr.Append("</td></tr>");
            }
            menulist = sbr.ToString();
        }

        private List<string> userpower()
        {
            try
            {
                if (!Funcs.IsNumber(userid))
                {
                    return null;
                }
                string strSql = "select id,menuid from system_power where userid=" + userid;
                DataTable dt = SqlHelper.GetDataTable(strSql);
                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item["menuid"].ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}