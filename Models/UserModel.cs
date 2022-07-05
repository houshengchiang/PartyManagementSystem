using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Models
{
    public class UserModel
    {
        public string UserId { get; set; }

        public string NickName { get; set; }

        public List<string> JoinedRooms { get; set; }
        
    }
}