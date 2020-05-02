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
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult Index()
        {
            var session = (LoginModel)Session[Constants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpGet]
        public JsonResult LoadData()
        {
            var session = (LoginModel)Session[Constants.USER_SESSION];
            var result = new BasketDao().GetCatalogInCart(session.username);
            return Json(new
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddToCart(int catalogid)
        {
            var session = (LoginModel)Session[Constants.USER_SESSION];
            if (session == null)
            {
                return Json(new
                {
                    status = -1
                });
            }
            var check = new BasketDao().AddToCart(catalogid, session.username);
            return Json(new
            {
                status = check
            });
        }
    }
}