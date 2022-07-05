using PartyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index([Bind(Prefix = "Id")] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.ResultModel = new ResultModel { Success = false, Message = "系統錯誤：無包廂ID" };
            }

            // 取得包廂資訊


            return View();
        }
    }
}