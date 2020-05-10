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
    public class CatalogController : AuthController
    {
        // GET: Admin/Catalog
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult GetAll(string name, int idbrand, int idtype, int status, int page, int pageSize)
        {
            var catalogDao = new CatalogDao();
            var result = catalogDao.GetAll(name, idbrand, idtype, status, page, pageSize);
            var types = catalogDao.GetCatalogType();
            return Json(new
            {
                totalRow = result.TotalRecord,
                data = result.Items,
                types = types.Items
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllType()
        {
            var catalogDao = new CatalogDao();
            var types = catalogDao.GetCatalogType();
            return Json(new
            {
                types = types.Items
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllBrand()
        {
            var catalogDao = new CatalogDao();
            var brands = catalogDao.GetCatalogBrand();
            return Json(new
            {
                brands = brands.Items
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadDetail(int id)
        {
            var catalogDao = new CatalogDao();
            var catalog = catalogDao.GetCatalogById(id);
            return Json(new
            {
                data = catalog
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCatalog(int id, int type, string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var catalogDao = new CatalogDao();
            var check = false;
            if (type == 1)
            {
                var catalogDTO = serializer.Deserialize<MobileDTO>(model);
                check = catalogDao.SaveMobile(id, catalogDTO);
            }
            else if (type == 2)
            {
                var catalogDTO = serializer.Deserialize<LaptopDTO>(model);
                check = catalogDao.SaveLaptop(id, catalogDTO);
            }
            return Json(new
            {
                status = check
            });
        }

        [HttpGet]
        public JsonResult DeleteCatalog(int id)
        {
            var check = new CatalogDao().DeleteCatalog(id);
            return Json(new
            {
                status = check
            }, JsonRequestBehavior.AllowGet);
        }
    }
}