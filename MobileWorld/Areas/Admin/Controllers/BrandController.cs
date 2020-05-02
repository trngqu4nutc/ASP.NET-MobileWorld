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
    public class BrandController : AuthController
    {
        // GET: Admin/Brand
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(string brand, int page, int pageSize)
        {
            var result = new BrandDao().GetListBrand(brand, page, pageSize);
            return Json(new
            {
                data = result.Items,
                totalRow = result.TotalRecord
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteBrand(int id)
        {
            var check = new BrandDao().DeleteBrand(id);
            return Json(new
            {
                status = check
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveBrand(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var brand = serializer.Deserialize<BrandDTO>(model);
            var check = new BrandDao().SaveBrand(brand);
            return Json(new
            {
                status = check
            });
        }
        [HttpGet]
        public JsonResult LoadDetail(int id)
        {
            var result = new BrandDao().LoadDetail(id);
            return Json(new
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}