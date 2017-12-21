using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Article
{
    public class PrevNext
    {
        /// <summary>
        /// 类型（1上一篇，2下一篇）
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 文章id
        /// </summary>
        public int articleId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string title { get; set; }
    }
}