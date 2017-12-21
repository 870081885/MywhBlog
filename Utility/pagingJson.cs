using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class pagingJson
    {
        /// <summary>
        /// 请求次数，直接获取返回
        /// </summary>
        public int draw = 0;

        /// <summary>
        /// 记录总条数
        /// </summary>
        public int recordsTotal = 0;

        /// <summary>
        /// 记录过滤条数
        /// </summary>
        public int recordsFiltered = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public object data = null;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string error = "";
    }
}
