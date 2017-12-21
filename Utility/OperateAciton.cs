using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 操作动作类(中介者模式)
    /// </summary>
    public class OperateAciton
    {
        //private string _operate;

        //public string Operate { get; set; }

        private Dictionary<string, Func<string>> _operateDictionary = new Dictionary<string, Func<string>>();

        public OperateAciton()
        { }

        //public OperateAciton(string operate)
        //{
        //    _operate = operate;
        //}

        /// <summary>
        /// 收集操作
        /// </summary>
        /// <param name="operate">操作名</param>
        /// <param name="action">动作(方法)</param>
        public void GatherOperate(string operate, Func<string> action)
        {
            if (!_operateDictionary.ContainsKey(operate))
                _operateDictionary.Add(operate, action);
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="operate">操作名</param>
        /// <returns></returns>
        public string ExecuteOperate(string operate)
        {
            if (_operateDictionary.ContainsKey(operate))
            {
                return _operateDictionary[operate]();
            }
            else
                throw new ArgumentException("无效的操作命令：{0}", operate);
        }
    }
}
