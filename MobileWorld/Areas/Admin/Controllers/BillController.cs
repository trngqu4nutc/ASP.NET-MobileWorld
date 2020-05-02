using Model.Dao;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MobileWorld.Areas.Admin.Controllers
{
    public class BillController : AuthController
    {
        // GET: Admin/Bill
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(string seach, int status, int page, int pageSize)
        {
            var result = new BillDao().LoadData(seach, status, page, pageSize);
            return Json(new
            {
                totalRow = result.TotalRecord,
                data = result.Items
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteBill(int id)
        {
            var check = new BillDao().DeleteBill(id);
            return Json(new
            {
                status = check
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateBill(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var bill = serializer.Deserialize<BillDTO>(model);
            var check = new BillDao().UpdateBill(bill.id, bill.status);
            return Json(new
            {
                status = check
            }, JsonRequestBehavior.AllowGet);
        }
    }
}