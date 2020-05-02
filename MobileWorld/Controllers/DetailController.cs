using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWorld.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index(int id)
        {
            return View(new CatalogDao().GetCatalogDetail(id));
        }
    }
}