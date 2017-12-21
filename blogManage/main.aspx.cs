using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Utility;
using blogManage.baseClass;

namespace blogManage
{
    public partial class main : BasicPage
    {
        public static int userId = 0;
        public static string userName = "", userType = "", dateTime = "";
        public string MenuList = "";
        StringBuilder sbr = new StringBuilder();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userId = MySession.GetUserID();
                userName = MySession.GetSessionStringValue("userName");
                userType = MySession.GetSessionStringValue("userTypeName");
                dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                bindMenu();
            }
        }

        //绑定菜单
        private void bindMenu()
        {
            string strSql = "";
            if (userId == 1) //管理员 
            {
                strSql = "select * from system_menu order by sortvalue";
            }
            else
            {
                strSql += "select system_menu.* from system_menu where id in( ";
                strSql += "select distinct menuId from system_power p inner join system_menu m on p.menuid=m.id where p.userid=" + userId + ")";
                strSql += "order by sortvalue";
            }
            dt = SqlHelper.GetDataTable(strSql);
            DataRow[] rows = dt.Select("menuType = 0");   //赋权限每个角色都必须有父节点的权限，否则一个都不输出了
            MenuBuild(0);
            MenuList = sbr.ToString();
        }


        //<li class="active">
        //    <a href="index.html"><i class="fa fa-th-large"></i> <span class="nav-label">Dashboards</span> <span class="fa arrow"></span></a>
        //    <ul class="nav nav-second-level">
        //        <li class="active"><a href="index.html">Dashboard v.1</a></li>
        //        <li><a href="dashboard_2.html">Dashboard v.2</a></li>
        //    </ul>
        //</li>
        //<li>
        //    <a href="layouts.html"><i class="fa fa-diamond"></i> <span class="nav-label">Layouts</span></a>
        //</li>

        //构建菜单
        public void MenuBuild(int menuId)
        {
            DataRow[] ListFirst = dt.Select(string.Format("parentId={0}", menuId));
            for (int i = 0; i < ListFirst.Length; i++)
            {
                DataRow[] firstClildren = dt.Select(string.Format("parentId={0}", Convert.ToInt32(ListFirst[i]["id"])));
                if (firstClildren.Length != 0)
                {
                    sbr.Append("<li>");
                    sbr.Append("<a href=\"#\">");
                    sbr.Append("<i class=\"" + ListFirst[i]["menuIcon"].ToString() + "\"></i>&nbsp;&nbsp;");
                    sbr.Append("<span class=\"nav-label\">" + ListFirst[i]["menuName"].ToString() + "</span><span class=\"fa arrow\"></span></a>");
                    sbr.Append("<ul class=\"nav nav-second-level\">");
                    //构建二级菜单
                    DataRow[] ListSecond = dt.Select(string.Format("parentId={0}", ListFirst[i]["id"].ToString()));
                    for (int j = 0; j < ListSecond.Length; j++)
                    {
                        DataRow[] secondClildren = dt.Select(string.Format("parentId={0}", Convert.ToInt32(ListSecond[j]["id"])));
                        if (secondClildren.Length != 0)
                        {
                            sbr.Append("<li>");
                            sbr.Append("<a href=\"#\" id=\"damian\">");
                            sbr.Append( ListSecond[j]["menuName"].ToString() + "<span class=\"fa arrow\"></span></a>");
                            sbr.Append("<ul class=\"nav nav-third-level\">");
                            //构建三级菜单
                            DataRow[] ListThird = dt.Select(string.Format("parentId={0}", ListSecond[j]["id"].ToString()));
                            for (int k = 0; k < ListThird.Length; k++)
                            {
                                sbr.Append("<li>");
                                sbr.Append("<a href=\"#\" title=\"" + ListThird[k]["menuUrl"].ToString() + "\">");
                                sbr.Append(ListThird[k]["menuName"].ToString() + "</a>");
                                sbr.Append("</li>");
                            }
                            sbr.Append("</ul>");
                            sbr.Append("</li>");
                        }
                        else
                        {
                            sbr.Append("<li>");
                            sbr.Append("<a href=\"#\" title=\"" + ListSecond[j]["menuUrl"].ToString() + "\">");
                            sbr.Append(ListSecond[j]["menuName"].ToString() + "</a>");
                            sbr.Append("</li>");
                        }
                    }
                    sbr.Append("</ul>");
                    sbr.Append("</li>");
                }
                else
                {                  
                    sbr.Append("<li>");
                    sbr.Append("<a href=\"#\" title=\"" + ListFirst[i]["menuUrl"].ToString() + "\">");
                    sbr.Append("<i class=\"" + ListFirst[i]["menuIcon"].ToString() + "\"></i>");
                    sbr.Append("<span class=\"nav-label\">" + ListFirst[i]["menuName"].ToString() + "</span></a>");
                    sbr.Append("</li>");
                }
            }
        }
    }
}