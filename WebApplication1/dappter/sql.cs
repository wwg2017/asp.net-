using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
namespace WebApplication1.dappter
{
    public class sql
    {
        //符合一般的这种连接 ，
        //public class b_base
        //{
        //    public IDbConnection Connection = null;
        //    public b_base()
        //    {
        //        Connection = new SqlConnection(ConfigurationManager.AppSettings["QlwMysqlConn"]);
        //    }
        //}
        //public class sql : b_base
        //{
        //    //public int InsertWithSql()
        //    //{
        //    //    using (var conn = Connection)
        //    //    {
        //    //        string _sql = "INSERT INTO t_department(departmentname,introduce,[enable])VALUES('应用开发部SQL','应用开发部主要开始公司的应用平台',1)";
        //    //        conn.Open();
        //    //        return conn.Execute(_sql);
        //    //    }
        //    //}  
        //mysql.data连接   
     
            private static readonly string Connection = ConfigurationManager.AppSettings["QlwMysqlConn"];
            public static IDbConnection CreateConnection(int databaseOption = 1)
            {

                IDbConnection conn = null;
                switch (databaseOption)
                {
                    case 1:
                        conn = new MySqlConnection(Connection);
                        break;
                }
                conn.Open();
                return conn;
            }

        }
        public class DatH : sql
        {
            public static IEnumerable<object> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
            {
                using (var conn = CreateConnection(1))
                {
                    return conn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
                }
            }
        }
    
}