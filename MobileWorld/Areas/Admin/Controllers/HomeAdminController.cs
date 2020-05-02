using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWorld.Areas.Admin.Controllers
{
    public class HomeAdminController : AuthController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}