using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Common
{
    public class DapperHelper
    {
        readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PartyManagement"].ConnectionString;

        /// <summary>
        /// 讀取全部資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> ReadAll<T>(string sql)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var result = conn.Query<T>(sql);
                return result.ToList();
            }
        }

        /// <summary>
        /// 讀取多筆資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> ReadMany<T>(string sql, object param)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var result = conn.Query<T>(sql,param);
                return result.ToList();
            }
        }

        /// <summary>
        /// 讀取一筆資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T ReadOne<T>(string sql, object param)
        {
            using (var conn = new SqlConnection(connectionString))
            {                
                var result = conn.Query<T>(sql, param).FirstOrDefault();
                return result;
            }
        }

        public int Execute(string sql, object param)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var result = conn.Execute(sql, param);
                return result;
            }
        }
    }
}