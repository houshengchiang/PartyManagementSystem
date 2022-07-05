using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.ViewModels
{
    public class UserViewModel
    {
        public string unid { get; set; }

        public string UserID { get; set; }

        public string Secret { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyTime { get; set; }

    }
}