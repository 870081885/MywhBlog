using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Base
{
    public class Paging<T>
    {
        /// <summary>
        /// 列表
        /// </summary>
        public IList<T> list { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPages { get; set; }

    }
}