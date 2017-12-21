using mywhBlog.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Article
{
    public class ArticleList<T>
    {
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<T> paging { get; set; }

        /// <summary>
        /// 头部信息
        /// </summary>
        public Head head { get; set; }

    }
}