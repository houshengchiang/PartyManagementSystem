using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.ViewModels
{
    public class RoomViewModel
    {
        // unid
        public Guid unid { get; set; }

        // 包廂ID
        public string RoomID { get; set; }
        
        // 包廂名稱
        public string RoomName { get; set; }

        // 包廂密碼
        public string Secret { get; set; }

        // 建立時間
        public DateTime? CreateTime { get; set; }

        // 建立人員
        public string CreateUser { get; set; }

        // 更新時間
        public DateTime? ModifyTime { get; set; }

        // 建立人員
        public string ModifyUser { get; set; }

        // 包廂人數
        public int UserCount { get; set; }
    }
}