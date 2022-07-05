using Dapper;
using PartyManagementSystem.Common;
using PartyManagementSystem.Models;
using PartyManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Services
{
    public class RoomService
    {
        readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PartyManagement"].ConnectionString;

        readonly DapperHelper _dapperHelper = new DapperHelper();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roomViewModel"></param>
        /// <returns></returns>
        public RoomViewModel Create(RoomViewModel roomViewModel)
        {            
            using(var conn = new SqlConnection(connectionString))
            {
                var sql = string.Empty;
                List<string> cols = new List<string>();
                List<string> vals = new List<string>();                
                DynamicParameters param = new DynamicParameters();

                if (!string.IsNullOrEmpty(roomViewModel.RoomID))
                {
                    cols.Add("RoomID");
                    vals.Add("@RoomID");
                    param.Add("@RoomID", roomViewModel.RoomID);
                }
                if (!string.IsNullOrEmpty(roomViewModel.RoomName))
                {
                    cols.Add("RoomName");
                    vals.Add("@RoomName");
                    param.Add("@RoomName", roomViewModel.RoomName);
                }
                if (!string.IsNullOrEmpty(roomViewModel.Secret))
                {
                    cols.Add("RoomSecret");
                    vals.Add("@RoomSecret");
                    param.Add("@RoomSecret", roomViewModel.Secret);
                }
                if (!string.IsNullOrEmpty(roomViewModel.CreateUser))
                {
                    cols.Add("CreateUser");
                    vals.Add("@CreateUser");
                    param.Add("@CreateUser", roomViewModel.CreateUser);
                }
                if (!string.IsNullOrEmpty(roomViewModel.ModifyUser))
                {
                    cols.Add("ModifyUser");
                    vals.Add("@ModifyUser");
                    param.Add("@ModifyUser", roomViewModel.ModifyUser);
                }


                sql = @"INSERT INTO T_Room (" + string.Join(", ", cols) + @") VALUES (" + string.Join(", ", vals) + @")";

                RoomViewModel result = conn.ExecuteScalar<RoomViewModel>(sql, param);

               return result;                
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public List<RoomViewModel> Query(string roomId, string userId = "")
        {
            using (var conn = new SqlConnection(connectionString))
            {
                List<string> sqlWhere = new List<string>();

                var sql = @"SELECT R.* FROM T_Room R
JOIN T_RoomDetail RD ON RD.RoomID = R.RoomID";

                DynamicParameters param = new DynamicParameters();

                if (!string.IsNullOrEmpty(roomId))
                {
                    sqlWhere.Add("R.RoomID = @RoomID");                   
                    param.Add("@RoomID", roomId);
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    sqlWhere.Add("RD.UserID = @UserID");
                    param.Add("@UserID", userId);
                }

                if (sqlWhere.Count() > 0)
                {
                    sql += " WHERE " + string.Join(" AND ", sqlWhere);
                }                

                List<RoomViewModel> result = (List<RoomViewModel>)conn.Query<RoomViewModel>(sql, param);

                return result;
            }
        }


        public RoomViewModel GetRoomInfo(string roomId)
        {
            var sql = @"
SELECT unid, RoomID, RoomName, RoomSecret, CreateTime, CreateUser, ModifyTime, ModifyUser FROM [T_Room] WHERE RoomID = @RoomID";
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoomID", roomId);

            var result = _dapperHelper.ReadOne<RoomViewModel>(sql, param);

            return result;
        }

        /// <summary>
        /// 取得閒置ID
        /// </summary>
        /// <returns></returns>
        public string GetFreeRoomID()
        {
            var roomID = string.Empty;
            int rnd;
            List<string> roomInUse = new List<string>();

            // 從DB讀取已使用的RoomID
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"SELECT [RoomID] FROM [T_Room]";

                roomInUse = (List<string>)conn.Query<string>(sql);
            }

            // 隨機取七位數
            do
            {
                rnd = new Random().Next(0, 9999999);
            }
            while (roomInUse.Contains(rnd.ToString("0000000")));
            
            return rnd.ToString("0000000");
        }

        public List<string> QueryMyRoom(string userId)
        {
            string sql = @"
SELECT RoomID FROM T_RoomDetail WHERE UserID = @UserID";

            DynamicParameters param = new DynamicParameters();
            param.Add("@UserID", userId);

            var result = _dapperHelper.ReadMany<string>(sql, param);

            return result.ToList();            
        }

        public void Join(string userId, string roomID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"
INSERT INTO T_RoomDetail (RoomID, UserID, UserLevel, CreateTime, ModifyUser, ModifyTime)
VALUES(@RoomID, @UserID, 3, GETDATE(), @ModifyUser, GETDATE())";

                DynamicParameters param = new DynamicParameters();
                param.Add("@RoomID", roomID);
                param.Add("@UserID", userId);
                param.Add("@ModifyUser", userId);

                conn.ExecuteScalar(sql, param);
            }

            UpdateUserCount(roomID);
        }

        /// <summary>
        /// 更新房間主表人數
        /// </summary>
        /// <param name="roomId"></param>
        public void UpdateUserCount(string roomId)
        {
            var sql = "SELECT COUNT(1) FROM T_RoomDetail WHERE RoomId = @RoomId";
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoomId", roomId);

            int userCount = _dapperHelper.ReadMany<RoomViewModel>(sql, param).Count();

            sql = "UPDATE T_Room SET UserCount = @UserCount WHERE RoomId = @RoomId;";
            param.Add("@UserCount", userCount);

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, param);
            }
        }
    }
}

/*
    房間     總人數   建立時間
    0243465  2      2022-04-23 01:03:24.227

 
 
 */