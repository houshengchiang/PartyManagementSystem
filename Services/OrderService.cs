using Dapper;
using PartyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Services
{
    public class OrderService: baseService
    {        
        

        public void ReadList(string roomId)
        {

        }


        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="orderlist"></param>
        /// <returns></returns>
        public OrderList Create(OrderList orderlist)
        {
            List<string> cols = new List<string>();
            List<string> vals = new List<string>();
            List<string> errMsg = new List<string>();
            DateTime timeNow = DateTime.Now;
            DynamicParameters param = new DynamicParameters();

            // 商品名稱
            cols.Add("[OrderItem]");
            vals.Add("@OrderItem");
            param.Add("@OrderItem", orderlist.OrderItem);

            // 數量
            cols.Add("[Quantity]");
            vals.Add("@Quantity");
            param.Add("@Quantity", orderlist.Quantity);
            
            // 單價
            cols.Add("[UnitPrice]");
            vals.Add("@UnitPrice");
            param.Add("@UnitPrice", orderlist.UnitPrice);
            
            // 包廂ID
            cols.Add("[RoomId]");
            vals.Add("@RoomId");
            param.Add("@RoomId", orderlist.RoomId);            

            // 用戶名
            cols.Add("[UserName]");
            vals.Add("@UserName");
            param.Add("@UserName", orderlist.UserName);

            // 贊助者
            if (!string.IsNullOrEmpty(orderlist.Sponsor))
            {
                cols.Add("[Sponsor]");
                vals.Add("@Sponsor");
                param.Add("@Sponsor", orderlist.Sponsor);
            }

            if (orderlist.CanceledTime.HasValue)
            {
                cols.Add("[CanceledTime]");
                vals.Add("@CanceledTime");
                param.Add("@CanceledTime", orderlist.CanceledTime);
            }

            if (orderlist.isServed.HasValue)
            {
                cols.Add("[isServed]");
                vals.Add("@isServed");
                param.Add("@isServed", orderlist.isServed);
            }

            if (orderlist.isCanceled.HasValue)
            {
                cols.Add("[isCanceled]");
                vals.Add("@isCanceled");
                param.Add("@isCanceled", orderlist.isCanceled);
            }

            // 建立人員
            if (!string.IsNullOrEmpty(orderlist.CreateUser))
            {
                cols.Add("[CreateUser]");
                vals.Add("@CreateUser");
                param.Add("@CreateUser", orderlist.CreateUser);
            }

            // 建立時間
            cols.Add("[CreateTime]");
            vals.Add("@CreateTime");
            param.Add("@CreateTime", timeNow);

            // 更新人員
            cols.Add("[UpdateUser]");
            vals.Add("@UpdateUser");
            param.Add("@UpdateUser", orderlist.UpdateUser);
            
            // 更新時間
            cols.Add("[UpdateTime]");
            vals.Add("@UpdateTime");
            param.Add("@UpdateTime", timeNow);            

            // 備註
            if (!string.IsNullOrEmpty(orderlist.Remark))
            {
                cols.Add("[Remark]");
                vals.Add("@Remark");
                param.Add("@Remark", orderlist.Remark);
            }


            string sql = @"
INSERT INTO OrderList(" + string.Join(", ", cols) + @") VALUES
(" + string.Join(", ", cols) + ")";

            var result = GetDapperHelper().Execute(sql, param);
                        
            return orderlist;
                        
        }
    }
}