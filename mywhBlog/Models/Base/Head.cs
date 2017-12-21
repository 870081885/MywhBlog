using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Base
{
    public class Head
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string keywords { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

    }
}