using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 转换帮助类
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// DataTable 转 model list
        /// </summary>
        /// <param name="dt">表</param>
        /// <returns>返回list,值是model类</returns>
        public static IList<T> FillModelList<T>(DataTable dt)
        {
            // 定义集合    
            IList<T> list = new List<T>();
            // 获得此模型的类型    
            // Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                //T t = new T();
                T obj = Activator.CreateInstance<T>();
                // 获得此模型的公共属性    
                PropertyInfo[] propertys = obj.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列    
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter    
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (pi.GetMethod.ReturnParameter.ParameterType.Name == "Int32")
                            {
                                value = Convert.ToInt32(value);
                            }
                            pi.SetValue(obj, value, null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }


        /// <summary>  
        /// 填充对象：用DataRow填充实体类
        /// </summary>  
        public static T FillModel<T>(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            T model = (T)Activator.CreateInstance(typeof(T));  

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                {
                    object value = dr[i];
                    if (propertyInfo.GetMethod.ReturnParameter.ParameterType.Name == "Int32")
                    {
                        value = Convert.ToInt32(value);
                    }
                    if (propertyInfo.GetMethod.ReturnParameter.ParameterType.Name == "String")
                    {
                        value = value.ToString();
                    }
                    propertyInfo.SetValue(model, value, null);
                }
            }
            return model;
        }
    }
}
