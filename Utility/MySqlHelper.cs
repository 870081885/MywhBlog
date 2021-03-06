﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace Utility
{
    public static class MySqlHelper
    {
        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnString"].ToString().Trim();
        #endregion

        #region ExecuteNonQuery方法

        /// <summary>
        ///执行一个不需要返回值的MySqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="cmdType">MySqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此MySqlCommand命令执行后影响的行数</returns>
        private static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    //通过PrePareCommand方法将参数逐个加入到MySqlCommand的参数集合中
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    //清空MySqlCommand中的参数列表
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        ///存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此MySqlCommand命令执行后影响的行数</returns>
        public static int ExecProcNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        ///Sql语句专用
        /// </summary>
        /// <param name="cmdText">T_Sql语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此MySqlCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, cmdText, commandParameters);
        }

        #endregion

        #region GetDataTable方法

        /// <summary>
        /// 执行一条返回结果集的MySqlCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="cmdTye">MySqlCommand命令类型</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表,表示查询得到的数据</returns>
        private static DataTable GetDataTable(string connectionString, CommandType cmdTye, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                    //清空SqlCommand中的参数列表
                    cmd.Parameters.Clear();
                    DataTable table = ds.Tables[0];
                    return table;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表,表示查询得到的数据</returns>
        public static DataTable ExecProcFillDataTable(string cmdText, params MySqlParameter[] commandParameters)
        {
            return GetDataTable(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// Sql语句专用
        /// </summary>
        /// <param name="cmdText"> T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集,表示查询得到的数据</returns>
        public static DataTable GetDataTable(string cmdText, params MySqlParameter[] commandParameters)
        {
            return GetDataTable(connectionString, CommandType.Text, cmdText, commandParameters);
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行一个MySqlCommand,返回一个结果集
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">MySqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        private static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //清空SqlCommand中的参数列表
                    cmd.Parameters.Clear();
                    return rdr;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region ExecuteDataSet方法

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdType">MySqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        private static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    //清空SqlCommand中的参数列表
                    cmd.Parameters.Clear();
                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdType">MySqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecProcFillDataSet(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }
        #endregion

        #region ExecuteScalar方法

        /// <summary>
        /// 执行一个MySqlCommand返回第一列的第一个记录
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">MySqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        private static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    //清空SqlCommand中的参数列表
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 返回第一行的第一列存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteProcScalar(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列Sql语句专用
        /// </summary>
        /// <param name="cmdText">者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供MySqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalar(string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(connectionString, CommandType.Text, cmdText, commandParameters);
        }
        #endregion

        #region Exists
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string strSql)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, strSql, null));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 为执行命令准备参数
        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">MySqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">MySqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion
    }
}
