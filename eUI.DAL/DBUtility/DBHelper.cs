using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace eUI.DAL.DBUtility
{
    public class DBHelper
    {

        public static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExcuteNoQuerySql(string cmdText)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                    //执行返回结果   
                    int iResult = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return iResult;
                }
                catch (Exception ex)
                {
                    //Response
                    return 0;
                }
            }
        }
        #endregion

        #region 查询数据库结果记录集
        /// <summary>
        /// 查询数据库结果记录集
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回DataTable</returns>
        public static DataTable SearchSql(string cmdText)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                MySqlDataAdapter sda = new MySqlDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
        }
        #endregion

        #region 返回最大值
        /// <summary>
        /// 返回最大值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExcuteScalar(string sqlText)
        {
            int count = 0;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                }

            }
            catch
            {
            }
            finally
            {
                //conn.Close();
                //conn.Dispose();
            }
            return count;
        }
        #endregion

        #region 返回首行首列
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ExcuteScalarFirstRow(string sqlText)
        {
            string content = "";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                    content = cmd.ExecuteScalar().ToString();
                    cmd.Dispose();
                }

            }
            catch
            {
            }
            finally
            {
                //conn.Close();
                //conn.Dispose();
            }
            return content;
        }
        #endregion

        #region 通用存储过程查询方法
        /// <summary>
        /// 通用存储过程查询方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataTable Pro_search(string ProName, MySqlParameter[] pars)
        {
            DataTable dt = new DataTable();
            dt.TableName = "tb";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(ProName, conn);
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1200;

                //conn.Open();
                //SqlDataReader rd = cmd.ExecuteReader();
                //dt.Load(rd);
                //rd.Close();
                //conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;

        }
        #endregion

        #region 通用存储过程查询方法
        /// <summary>
        /// 通用存储过程查询方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataSet Pro_searchDS(string ProName, MySqlParameter[] pars)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(ProName, conn);
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1200;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }

            return ds;

        }
        #endregion

        #region 通用存储过程返回首行首列
        /// <summary>
        /// 通用存储过程查询方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static string Pro_ScalarFirstRow(string ProName, MySqlParameter[] pars)
        {
            string content = "";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(ProName, conn);
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }

                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                content = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
            }
            return content;

        }
        #endregion

        #region 通用存储过程执行方法
        /// <summary>
        /// 通用存储过程执行方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int Pro_ExecSql(string ProName, MySqlParameter[] pars)
        {
            int row = 0;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(ProName, conn);

                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                row = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return row;

        }
        #endregion
    }
}
