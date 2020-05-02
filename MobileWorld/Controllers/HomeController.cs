using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWorld.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CatalogLatest = new CatalogDao().LoadLatest();
            ViewBag.CatalogSlide = new CatalogDao().LoadSlide();
            return View();
        }

    }
}