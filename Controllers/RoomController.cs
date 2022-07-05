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
    public class RoomController : Controller
    {

        RoomService _roomServices = new RoomService();
        
        public ActionResult Index()
        {
            UserViewModel user = (UserViewModel)Session["User"];

            if (string.IsNullOrEmpty(user.UserID))
            {
                ViewBag.ResultModel = new ResultModel { Success = false, Message = "系統錯誤，查無 房間ID" };
                return RedirectToAction("Query", "Room");
            }

            List<string> rooms = _roomServices.QueryMyRoom(user.UserID);

            List<RoomViewModel> roomViewModels = new List<RoomViewModel>();
            if (rooms.Count() > 0)
            {
                foreach(string roomId in rooms)
                {
                    roomViewModels.Add(_roomServices.GetRoomInfo(roomId));
                }
            }


            return View(roomViewModels);
        }

        /// <summary>
        /// 建立包廂
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Query", "Room");
            }

            RoomViewModel vm = new RoomViewModel();

            vm.RoomID = _roomServices.GetFreeRoomID();

            return View(vm);
        }

        [HttpPost]
        public JsonResult Create(RoomViewModel roomViewModel)
        {
            roomViewModel.CreateUser = (Session["User"] as UserViewModel).UserID;
            roomViewModel.ModifyUser = (Session["User"] as UserViewModel).UserID;
            ResultModel resultModel = new ResultModel();
            try
            {
                var result = _roomServices.Create(roomViewModel);
                
                _roomServices.Join(roomViewModel.CreateUser, roomViewModel.RoomID);                

                resultModel = new ResultModel { Message = "", ReturnObject = roomViewModel };
            }
            catch (Exception ex)
            {
                resultModel = new ResultModel { Success = false, Message = ex.Message };
            }

            return Json(resultModel);
        }

        public ActionResult grid(string roomId = "", string userId = "")
        {
            var viewModel = _roomServices.Query(roomId, userId);
            return PartialView(viewModel);
        }

        /// <summary>
        /// 查詢畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {            
            return View();
        }

        [HttpPost]
        public JsonResult JoinRoom(string roomId)
        {
            try
            {
                _roomServices.Join((Session["User"] as UserViewModel).UserID, roomId);
                return Json(new ResultModel { Message = "成功加入" });
            }
            catch(Exception ex)
            {
                return Json(new ResultModel { Success = false, Message = ex.Message, ReturnObject = ex });
            }
        }

        public ActionResult RoomDetail(string roomId)
        {
            //roomId = "7493649";

            if (string.IsNullOrEmpty(roomId))
            {
                ViewBag.ResultModel = new ResultModel { Success = false, Message = "查無房間ID" };
                return View(new RoomViewModel());
            }

            RoomViewModel roomViewModel = _roomServices.GetRoomInfo(roomId);

            return View(roomViewModel);
        }
    }
}