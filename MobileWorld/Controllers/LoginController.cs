using MobileWorld.Common;
using MobileWorld.Models;
using Model.Dao;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MobileWorld.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult Signin(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            LoginModel loginModel = serializer.Deserialize<LoginModel>(model);
            var userDao = new UserDao();
            int check = userDao.Login(loginModel.username, loginModel.password);
            if(check == 1)
            {
                loginModel.role = userDao.GetRole(loginModel.username);
                Session.Add(Constants.USER_SESSION, loginModel);
                return Json(new
                {
                    status = 1,
                    role = loginModel.role
                });
            }else if(check == 0)
            {
                return Json(new
                {
                    status = 0
                });
            }
            else if (check == -2)
            {
                return Json(new
                {
                    status = -2
                });
            }
            else
            {
                return Json(new
                {
                    status = -1
                });
            }
        }

        [HttpPost]
        public JsonResult Register(string model)
        {
            var userDao = new UserDao();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var userDTO = serializer.Deserialize<UserDTO>(model);
            var check = userDao.Insert(userDTO);
            return Json(new
            {
                status = check
            });
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Remove(Constants.USER_SESSION);
            return Json(new
            {
                status = true
            });
        }
        
    }
}