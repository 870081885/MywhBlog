using mywhBlog.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Article
{
    public class ArticleContent
    {
        public Base.Article article { get; set; }

        public IList<ArticleTag>  articleTag { get; set; }

        public IList<PrevNext> prevNext { get; set; }

    }
}