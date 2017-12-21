using mywhBlog.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace mywhBlog.Controllers
{
    public class MenuController : Controller
    {
        StringBuilder strbMenu = new StringBuilder();
        DataTable dtMenu = null;
        // GET: Menu
        public ActionResult BindMenu()
        {
            MenuDAL dal = new MenuDAL();
            dtMenu = dal.getMenuList();
            MenuBuild(0);
            ViewBag.strMenu= strbMenu.ToString();
            return PartialView();
        }

        /// <summary>
        /// 构建菜单
        /// </summary>
        /// <param name="menuId"></param>
        private void MenuBuild(int menuId)
        {
            DataRow[] ListFirst = dtMenu.Select(string.Format("parentId={0}", menuId));
            for (int i = 0; i < ListFirst.Length; i++)
            {
                DataRow[] firstClildren = dtMenu.Select(string.Format("parentId={0}", Convert.ToInt32(ListFirst[i]["id"])));
                if (firstClildren.Length != 0)
                {

                    strbMenu.Append("<li>");
                    strbMenu.Append("<a href=\"/" + ListFirst[i]["menuKey"].ToString() + "/\" rel=\"nofollow\">" + ListFirst[i]["menuName"].ToString() + "</a>");
                    strbMenu.Append("<ul>");
                    //构建二级菜单
                    DataRow[] ListSecond = dtMenu.Select(string.Format("parentId={0}", ListFirst[i]["id"].ToString()));
                    for (int j = 0; j < ListSecond.Length; j++)
                    {
                        strbMenu.Append("<li>");
                        strbMenu.Append("<a href=\"/" + ListSecond[j]["menuKey"].ToString() + "/\" rel=\"nofollow\">" + ListSecond[j]["menuName"].ToString() + "</a>");
                        strbMenu.Append("</li>");
                    }
                    strbMenu.Append("</ul>");
                    strbMenu.Append("</li>");
                }
                else
                {
                    strbMenu.Append("<li>");
                    if (ListFirst[i]["menuKey"].ToString() == "")
                    {
                        strbMenu.Append("<a href=\"/\">" + ListFirst[i]["menuName"].ToString() + "</a>");
                    }
                    else
                    {
                        strbMenu.Append("<a href=\"/" + ListFirst[i]["menuKey"].ToString() + "/\">" + ListFirst[i]["menuName"].ToString() + "</a>");
                    }
                    strbMenu.Append("</li>");
                }
            }
        }
    }
}