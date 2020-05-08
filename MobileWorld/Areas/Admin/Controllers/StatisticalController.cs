using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWorld.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        public ActionResult Index(string name)
        {
            if(name == "san-pham")
            {
                return View();
            }
            return View();
        }
        [HttpGet]
        public JsonResult GetAllCatalog(string seach, int brandid, int month, int page, int pageSize)
        {
            var result = new StatisticalDao().GetAllCatalog(seach, brandid, month, page, pageSize);
            return Json(new
            {
                totalMoney = result.Total,
                totalRow = result.TotalRecord,
                data = result.Items
            }, JsonRequestBehavior.AllowGet);
        }
    }
}