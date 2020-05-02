using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;

namespace MobileWorld.Controllers
{
    public class HomeCatalogController : Controller
    {
        public ActionResult Mobile()
        {
            return View();
        }

        public ActionResult Laptop()
        {
            return View();
        }

        public JsonResult LoadData(int type, int cost, string brand, int index)
        {
            var result = new CatalogDao().LoadHomeMobile(type, cost, brand, index);
            var brands = new BrandDao().GetBrand(1);
            return Json(new
            {
                total = result.TotalRecord,
                data = result.Items
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadBrand(int type)
        {
            var brands = new BrandDao().GetBrand(type);
            return Json(new
            {
                brands = brands
            }, JsonRequestBehavior.AllowGet);
        }

    }
}