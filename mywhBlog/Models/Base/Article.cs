using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mywhBlog.Models.Base
{
    public class Article
    {
        /// <summary>
        /// 文章id
        /// </summary>
        public int articleId { get; set; }

        /// <summary>
        /// 文章名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// 文章介绍
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 文章类型Id
        /// </summary>
        public string categoryId { get; set; }

        /// <summary>
        /// 文章类型名称
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int browserNum { get; set; }

        /// <summary>
        /// 菜单key
        /// </summary>
        public string menuKey { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createTime { get; set; }
    }
}