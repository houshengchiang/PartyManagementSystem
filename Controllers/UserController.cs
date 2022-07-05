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
    public class UserController : Controller
    {
        UserService _userService = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 建立使用者
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(UserViewModel userVM)
        {
            bool isIDAvailable = _userService.isUserIDAvailable(userVM.UserID);
            if (!isIDAvailable)
            {
                return Json(new ResultModel { Success = false, Message = "此ID已存在" });
            }

            bool isEmailAvailable = _userService.isEmailAvailable(userVM.Email);
            if (!isEmailAvailable)
            {
                return Json(new ResultModel { Success = false, Message = "此Email已存在" });
            }

            var result = _userService.CreateUser(userVM);

            return Json(new ResultModel { Message = "成功加入!!", ReturnObject = result });
        }
    }
}