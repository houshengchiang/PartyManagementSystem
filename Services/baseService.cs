using PartyManagementSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Services
{
    public class baseService
    {


        protected DapperHelper GetDapperHelper()
        {
            return new DapperHelper();
        }
    }
}