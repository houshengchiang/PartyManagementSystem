using PartyManagementSystem.Models;
using PartyManagementSystem.Services;
using PartyManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyManagementSystem.Controllers
{
    public class SecurityController : Controller
    {
        UserService _userServices = new UserService();


        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SignIn(UserViewModel userVM)
        {            
            userVM = _userServices.GetUser(userVM.UserID, userVM.Secret);
            
            if (userVM == null)
            {
                Session.Remove("User");
                return Json(new ResultModel { Success = false, Message = "登入失敗" });                
            }

            Session["User"] = userVM;
            return Json(new ResultModel { Success = true, Message = "登入成功", ReturnObject = new { userId = userVM.UserID} });
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public JsonResult SignOut()
        {
            Session.Abandon();

            return Json(new ResultModel { Success = true, Message = "登出成功" });
        }
    }
}