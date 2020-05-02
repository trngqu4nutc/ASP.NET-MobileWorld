using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using System.Web.Script.Serialization;
using Model.DTO;

namespace MobileWorld.Areas.Admin.Controllers
{
    public class UserController : AuthController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            //var userDao = new UserDao();
            //var model = userDao.ListAllPaging(page, pageSize);
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(string name, string status, int page, int pageSize)
        {
            var userDao = new UserDao();
            var model = userDao.ListAllPaging(name, status, page, pageSize);
            return Json(new
            {
                status = true,
                totalRow = model.TotalRecord,
                data = model.Items
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            if (model != null)
            {
                var userDao = new UserDao();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var userDTO = serializer.Deserialize<UserDTO>(model);
                bool check = true;
                if(userDTO.id != 0)
                {
                    check = userDao.Update(userDTO);
                }
                else
                {
                    check = userDao.Insert(userDTO);
                }
                if (check)
                {
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new {
                status = result
            });
        }
        [HttpGet]
        public JsonResult LoadDetail(int id)
        {
            var model = new UserDao().GetUserById(id);
            return Json(new
            {
                status = true,
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}