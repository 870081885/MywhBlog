using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Base
{
    public class ArticleTag
    {
        /// <summary>
        /// 文章id
        /// </summary>
        public int articleId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string tagName { get; set; }
    }
}