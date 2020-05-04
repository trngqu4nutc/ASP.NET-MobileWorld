using EmailService;
using Facebook;
using MobileWorld.Common;
using MobileWorld.Models;
using Model.Dao;
using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MobileWorld.Controllers
{
    public class LoginController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginFaceBook()
        {
            var fb = new FacebookClient();
            var loginurl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                clienr_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginurl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string id = me.id;
                string fullname = me.last_name + " " + me.middle_name + " " + me.first_name;
                string email = me.email;
                var entity = new User()
                {
                    username = id,
                    fullname = fullname,
                    email = email,
                    status = true,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };
                var check = new UserDao().InsertFacebook(entity);
                if(check > 0)
                {
                    var user = new UserDao().GetByUserName(entity.username);
                    var userSession = new UserLogin()
                    {
                        username = entity.username,
                        fullname = user.fullname,
                        role = user.role
                    };
                    Session.Add(Constants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Forgot()
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
            if (check == 1)
            {
                var user = userDao.GetByUserName(loginModel.username);
                var userSession = new UserLogin()
                {
                    username = loginModel.username,
                    fullname = user.fullname,
                    role = user.role
                };
                Session.Add(Constants.USER_SESSION, userSession);
                return Json(new
                {
                    status = 1,
                    role = user.role
                });
            }
            else if (check == 0)
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
            if (check)
            {
                StringBuilder content = new StringBuilder("<p> Bạn đã đăng ký tài khoản <b>");
                content.Append(userDTO.username);
                content.Append("</b> thành công!</p>");
                new MailHelper().SendEmail(userDTO.email,
                    "Chào mừng bạn đến với MobileWorld!",
                    content.ToString());
            }
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

        [HttpPost]
        public JsonResult AuthAccount(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var userAuth = serializer.Deserialize<UserDTO>(model);
            var result = new UserDao().AuthAccount(userAuth);
            if(result.ContainsKey(1))
            {
                //var content = new StringBuilder("<p style=\"color: black;\">Tài khoản <b>");
                //content.Append(userAuth.username);
                //content.Append("</b> đã được thay đổi mật khẩu vào lúc ");
                //content.Append(DateTime.Now.ToString() + ".</p>");
                //content.Append("<p style=\"color: black;\">Hãy chắc chắn rằng đó là bạn và liên hệ tới hòm thư ");
                //content.Append(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                //content.Append(" để biết thêm thông tin chi tiết.</p>");
                //new MailHelper().SendEmail(userAuth.email, "Xác thực thay đổi mật khẩu!", content.ToString());
                var password = "";
                foreach(var item in result)
                {
                    password = item.Value;
                    break;
                }
                new MailHelper().SendEmail(userAuth.email,
                    "Your password has been changed.",
                    "Password: " + password);
            }
            return Json(new
            {
                status = result.Keys
            });
        }

    }
}