using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MobileWorld.Areas.Admin.Controllers
{
    public class HistoryController : AuthController
    {
        // GET: Admin/History
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAll(string seach, int brandid, int month, int page, int pageSize)
        {
            var result = new HistoryDao().GetAll(seach, brandid, month, page, pageSize);
            return Json(new
            {
                totalRow = result.TotalRecord,
                data = result.Items
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteHistory(int id)
        {
            var check = new HistoryDao().DeleteHistory(id);
            return Json(new
            {
                status = check
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteAllHistory(string ids)
        {
            var listId = new JavaScriptSerializer().Deserialize<List<int>>(ids);
            var check = new HistoryDao().DeleteHistorys(listId);
            return Json(new
            {
                status = check
            }, JsonRequestBehavior.AllowGet);
        }
    }
}