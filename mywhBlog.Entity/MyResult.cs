using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mywhBlog.Entity
{
    public class MyResult
    {
        /// <summary>
        /// flag:0失败 flag:1成功
        /// </summary>
        public int flag { get; set; }
        public string msg { get; set; }
        public object obj { get; set; }
        public object obj2 { get; set; }
        public object obj3 { get; set; }
    }
}
