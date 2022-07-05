using Dapper;
using PartyManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Services
{
    public class UserService
    {
        readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PartyManagement"].ConnectionString;        

        /// <summary>
        /// 建立使用者
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        public UserViewModel CreateUser(UserViewModel userVM)
        {

            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"
INSERT INTO T_User ([Email], [UserId], [Secret], [NickName], [CreateDate], [ModifyTime]) 
VALUES (@Email, @UserId, @Secret, @NickName, GETDATE(), GETDATE())";

                DynamicParameters param = new DynamicParameters();
                param.Add("@Email", userVM.Email);
                param.Add("@UserID", userVM.UserID);
                param.Add("@Secret", userVM.Secret);
                param.Add("@NickName", userVM.Nickname);

                var result = conn.ExecuteScalar<UserViewModel>(sql, param);

                return result;
            }
        }

        /// <summary>
        /// 檢查ID是否可用
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool isUserIDAvailable(string userId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"
SELECT UserID FROM T_User WHERE UserID = @UserID";

                DynamicParameters param = new DynamicParameters();                
                param.Add("@UserID", userId);
                
                var result = conn.Query<UserViewModel>(sql, param);

                return result.Count() == 0;
            }
        }

        /// <summary>
        /// 檢查Email是否可用
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool isEmailAvailable(string email)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"
SELECT Email FROM T_User WHERE Email = @Email";

                DynamicParameters param = new DynamicParameters();
                param.Add("@Email", email);

                var result = conn.Query<UserViewModel>(sql, param);

                return result.Count() == 0;
            }
        }

        /// <summary>
        /// 透過帳蜜取得身分
        /// </summary>
        /// <param name="id"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public UserViewModel GetUser(string id, string secret)
        {
            using (var conn= new SqlConnection(connectionString))
            {
                var sql = @"
SELECT CONVERT(varchar(36), unid), UserId, Nickname, Email FROM T_User WHERE UserId = @UserId AND Secret = @secret";

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", id);
                param.Add("@secret", secret);

                var result = conn.Query<UserViewModel>(sql, param).FirstOrDefault();

                return result;
            }
        }
    }
}