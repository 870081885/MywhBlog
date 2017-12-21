using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace mywhBlog.Business
{
    public class MenuDAL
    {
        public DataTable getMenuList()
        {
            string strSql = "select * from tb_blogMenu where status=1 order by parentId,sort";
            DataTable  dt = SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
