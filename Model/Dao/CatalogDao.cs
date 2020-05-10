using Model.DTO;
using Model.EF;
using Model.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Model.Dao
{
    public class CatalogDao
    {
        OnlineShopDbContext _context = null;

        public CatalogDao()
        {
            _context = new OnlineShopDbContext();
        }

        public PagedResult<CatalogDTO> GetAll(string name, int idbrand, int idtype, int status, int page, int pageSize)
        {
            var query = from c in _context.Catalogs
                        join sp in _context.Specifications on c.id equals sp.catalogid
                        select new { c, sp };
            if(status < 2)
            {
                if(status == 1)
                {
                    query = query.Where(x => x.c.status == true);
                }
                else
                {
                    query = query.Where(x => x.c.status == false);
                }
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.c.name.Contains(name));
            }
            if (idbrand != 0)
            {
                query = query.Where(x => x.c.catalogbrandid == idbrand);
            }
            if (idtype != 0)
            {
                query = query.Where(x => x.c.catalogtypeid == idtype);
            }
            PagedResult<CatalogDTO> result = new PagedResult<CatalogDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderBy(x => x.c.id).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new CatalogDTO()
                {
                    id = x.c.id,
                    cpu = x.sp.cpu,
                    name = x.c.name,
                    os = x.sp.os,
                    pictureuri = x.c.pictureuri,
                    price = x.c.price,
                    quantity = x.c.quantity,
                    status = x.c.status,
                    ram = x.sp.ram,
                    screen = x.sp.screen
                }).ToList();
            return result;
        }
        public PagedResult<CatalogTypeDTO> GetCatalogType()
        {
            var query = _context.CatalogTypes;
            PagedResult<CatalogTypeDTO> result = new PagedResult<CatalogTypeDTO>();
            result.Items = query.OrderBy(x => x.id)
                .Select(x => new CatalogTypeDTO()
                {
                    id = x.id,
                    type = x.type
                }).ToList();
            return result;
        }
        public PagedResult<CatalogBrandDTO> GetCatalogBrand()
        {
            var query = _context.CatalogBrands;
            PagedResult<CatalogBrandDTO> result = new PagedResult<CatalogBrandDTO>();
            result.Items = query.OrderBy(x => x.id)
                .Select(x => new CatalogBrandDTO()
                {
                    id = x.id,
                    brand = x.brand
                }).ToList();
            return result;
        }
        public CatalogDTO GetCatalogById(int id)
        {
            var model = _context.Catalogs.Find(id);
            if (model.catalogtypeid == 1)
            {
                var query = from c in _context.Catalogs
                            join t in _context.CatalogTypes on c.catalogtypeid equals t.id
                            join b in _context.CatalogBrands on c.catalogbrandid equals b.id
                            join sp in _context.Specifications on c.id equals sp.catalogid
                            join sm in _context.SpecificationsMobiles on c.id equals sm.catalogid
                            where c.id == id
                            select new { c, t, b, sp, sm };
                MobileDTO catalog = new MobileDTO();
                foreach(var item in query)
                {
                    catalog.id = item.c.id;
                    catalog.name = item.c.name;
                    catalog.pictureuri = item.c.pictureuri;
                    catalog.price = item.c.price;
                    catalog.description = item.c.description;
                    catalog.content = item.c.content;
                    catalog.quantity = item.c.quantity;
                    catalog.catalogbrandid = item.c.catalogbrandid;
                    catalog.catalogtypeid = item.c.catalogtypeid;
                    catalog.cpu = item.sp.cpu;
                    catalog.ram = item.sp.ram;
                    catalog.screen = item.sp.screen;
                    catalog.os = item.sp.os;
                    catalog.backcamera = item.sm.backcamera;
                    catalog.frontcamera = item.sm.frontcamera;
                    catalog.internalmemory = item.sm.internalmemory;
                    catalog.memorystick = item.sm.memorystick;
                    catalog.sim = item.sm.sim;
                    catalog.batery = item.sm.batery;
                }
                return catalog;
            }
            else
            {
                var query = from c in _context.Catalogs
                            join t in _context.CatalogTypes on c.catalogtypeid equals t.id
                            join b in _context.CatalogBrands on c.catalogbrandid equals b.id
                            join sp in _context.Specifications on c.id equals sp.catalogid
                            join sl in _context.SpecificationsLaptops on c.id equals sl.catalogid
                            where c.id == id
                            select new { c, t, b, sp, sl };
                LaptopDTO catalog = new LaptopDTO();
                foreach (var item in query)
                {
                    catalog.id = item.c.id;
                    catalog.name = item.c.name;
                    catalog.pictureuri = item.c.pictureuri;
                    catalog.price = item.c.price;
                    catalog.description = item.c.description;
                    catalog.content = item.c.content;
                    catalog.quantity = item.c.quantity;
                    catalog.catalogbrandid = item.c.catalogbrandid;
                    catalog.catalogtypeid = item.c.catalogtypeid;
                    catalog.cpu = item.sp.cpu;
                    catalog.ram = item.sp.ram;
                    catalog.screen = item.sp.screen;
                    catalog.os = item.sp.os;
                    catalog.cardscreen = item.sl.cardscreen;
                    catalog.connector = item.sl.connector;
                    catalog.harddrive = item.sl.harddrive;
                    catalog.design = item.sl.design;
                    catalog.size = item.sl.size;
                    catalog.release = item.sl.release;
                }
                return catalog;
            }
        }
        public CatalogDetail GetCatalogDetail(int id)
        {
            var model = _context.Catalogs.Find(id);
            if (model.catalogtypeid == 1)
            {
                var query = from c in _context.Catalogs
                            join t in _context.CatalogTypes on c.catalogtypeid equals t.id
                            join b in _context.CatalogBrands on c.catalogbrandid equals b.id
                            join sp in _context.Specifications on c.id equals sp.catalogid
                            join sm in _context.SpecificationsMobiles on c.id equals sm.catalogid
                            where c.id == id
                            select new { c, t, b, sp, sm };
                CatalogDetail catalog = new CatalogDetail();
                foreach (var item in query)
                {
                    catalog.id = item.c.id;
                    catalog.name = item.c.name;
                    catalog.pictureuri = item.c.pictureuri;
                    catalog.price = item.c.price;
                    catalog.description = item.c.description;
                    catalog.content = item.c.content;
                    catalog.quantity = item.c.quantity;
                    catalog.catalogbrandid = item.c.catalogbrandid;
                    catalog.catalogtypeid = item.c.catalogtypeid;
                    catalog.catalogtypename = item.t.type;
                    catalog.cpu = item.sp.cpu;
                    catalog.ram = item.sp.ram;
                    catalog.screen = item.sp.screen;
                    catalog.os = item.sp.os;
                    catalog.backcamera = item.sm.backcamera;
                    catalog.frontcamera = item.sm.frontcamera;
                    catalog.internalmemory = item.sm.internalmemory;
                    catalog.memorystick = item.sm.memorystick;
                    catalog.sim = item.sm.sim;
                    catalog.batery = item.sm.batery;
                }
                return catalog;
            }
            else
            {
                var query = from c in _context.Catalogs
                            join t in _context.CatalogTypes on c.catalogtypeid equals t.id
                            join b in _context.CatalogBrands on c.catalogbrandid equals b.id
                            join sp in _context.Specifications on c.id equals sp.catalogid
                            join sl in _context.SpecificationsLaptops on c.id equals sl.catalogid
                            where c.id == id
                            select new { c, t, b, sp, sl };
                CatalogDetail catalog = new CatalogDetail();
                foreach (var item in query)
                {
                    catalog.id = item.c.id;
                    catalog.name = item.c.name;
                    catalog.pictureuri = item.c.pictureuri;
                    catalog.price = item.c.price;
                    catalog.description = item.c.description;
                    catalog.content = item.c.content;
                    catalog.quantity = item.c.quantity;
                    catalog.catalogbrandid = item.c.catalogbrandid;
                    catalog.catalogtypeid = item.c.catalogtypeid;
                    catalog.catalogtypename = item.t.type;
                    catalog.cpu = item.sp.cpu;
                    catalog.ram = item.sp.ram;
                    catalog.screen = item.sp.screen;
                    catalog.os = item.sp.os;
                    catalog.cardscreen = item.sl.cardscreen;
                    catalog.connector = item.sl.connector;
                    catalog.harddrive = item.sl.harddrive;
                    catalog.design = item.sl.design;
                    catalog.size = item.sl.size;
                    catalog.release = item.sl.release;
                }
                return catalog;
            }
        }
        public bool SaveMobile(int id, MobileDTO catalogDTO)
        {
            DbContextTransaction transaction = _context.Database.BeginTransaction();
            var check = SaveCatalog(id, transaction, catalogDTO);
            if (!check)
            {
                return false;
            }
            if (id == 0)
            {
                var mobile = new SpecificationsMobile();
                mobile = CatalogMapper.toSpecificationsMobile(catalogDTO, mobile);
                try
                {
                    _context.SpecificationsMobiles.Add(mobile);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            else
            {
                var mobile = _context.SpecificationsMobiles.SingleOrDefault(x => x.catalogid == catalogDTO.id);
                if (mobile == null)
                {
                    transaction.Rollback();
                    return false;
                }
                mobile = CatalogMapper.toSpecificationsMobile(catalogDTO, mobile);
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }
        public bool SaveLaptop(int id,LaptopDTO catalogDTO)
        {
            DbContextTransaction transaction = _context.Database.BeginTransaction();
            var check = SaveCatalog(id, transaction, catalogDTO);
            if (!check)
            {
                return false;
            }
            if(id == 0)
            {
                var laptop = new SpecificationsLaptop();
                laptop = CatalogMapper.toSpecificationsLaptop(catalogDTO, laptop);
                try
                {
                    _context.SpecificationsLaptops.Add(laptop);
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            else
            {
                var laptop = _context.SpecificationsLaptops.SingleOrDefault(x => x.catalogid == catalogDTO.id);
                if(laptop == null)
                {
                    transaction.Rollback();
                    return false;
                }
                laptop = CatalogMapper.toSpecificationsLaptop(catalogDTO, laptop);
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        private bool SaveCatalog(int id, DbContextTransaction transaction, CatalogDTO catalogDTO)
        {
            if(id == 0)
            {
                var count = _context.Catalogs.Count(x => x.name == catalogDTO.name || x.pictureuri == catalogDTO.pictureuri);
                if (count > 0)
                {
                    return false;
                }
                var catalog = new Catalog();
                catalog = CatalogMapper.toCatalog(catalogDTO, catalog);
                try
                {
                    _context.Catalogs.Add(catalog);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
                catalogDTO.id = _context.Catalogs.SingleOrDefault(x => x.name == catalogDTO.name).id;
                var history = new History() {
                    catalogid = catalogDTO.id,
                    unit = catalogDTO.quantity,
                    inputprice = catalogDTO.price,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };
                var specification = new Specification();
                specification = CatalogMapper.toSpecification(catalogDTO, specification);
                try
                {
                    _context.Histories.Add(history);
                    _context.Specifications.Add(specification);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            else
            {
                var catalog = _context.Catalogs.Find(catalogDTO.id);
                var history = _context.Histories.FirstOrDefault(x => x.catalogid == catalog.id);
                if(history != null)
                {
                    if (catalogDTO.quantity > catalog.quantity)
                    {
                        int unit = catalogDTO.quantity - catalog.quantity;
                        var newHistory = new History()
                        {
                            catalogid = history.catalogid,
                            inputprice = history.inputprice,
                            unit = unit,
                            createdAt = DateTime.Now,
                            updatedAt = DateTime.Now
                        };
                        _context.Histories.Add(newHistory);
                    }
                }
                else
                {
                    if (catalogDTO.quantity > catalog.quantity)
                    {
                        int unit = catalogDTO.quantity - catalog.quantity;
                        var newHistory = new History()
                        {
                            catalogid = catalog.id,
                            inputprice = catalog.price - Convert.ToDecimal(0.05 * Convert.ToDouble(catalog.price)),
                            unit = unit,
                            createdAt = DateTime.Now,
                            updatedAt = DateTime.Now
                        };
                        _context.Histories.Add(newHistory);
                    }
                }
                var specification = _context.Specifications.SingleOrDefault(x => x.catalogid == catalogDTO.id);
                catalog = CatalogMapper.toCatalog(catalogDTO, catalog);
                specification = CatalogMapper.toSpecification(catalogDTO, specification);
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public bool DeleteCatalog(int id)
        {
            var catalog = _context.Catalogs.Find(id);
            if(catalog == null)
            {
                return false;
            }
            catalog.status = !catalog.status;
            catalog.updatedAt = DateTime.Now;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public List<Catalog> LoadLatest()
        {
            return _context.Catalogs.Where(x => x.catalogtypeid == 1).OrderByDescending(x => x.price).Take(6).ToList();
        }
        public List<Catalog> LoadSlide()
        {
            return _context.Catalogs.Where(x => x.catalogtypeid == 1).OrderByDescending(x => x.price).Take(4).ToList();
        }
        public PagedResult<CatalogDTO> LoadHomeMobile(int type, int cost, string brand, int index)
        {
            var query = _context.Catalogs.Where(x => x.catalogtypeid == type && x.status == true);
            var result = new PagedResult<CatalogDTO>();
            if(!brand.Equals("[]"))
            {
                query = query.Where(x => brand.Contains(x.catalogbrandid.ToString()));
            }
            if (cost > 0)
            {
                if (cost == 10)
                {
                    query = query.Where(x => x.price < 10000000);
                }
                else if (cost == 15)
                {
                    query = query.Where(x => x.price >= 10000000 && x.price < 15000000);
                }
                else if (cost == 25)
                {
                    query = query.Where(x => x.price >= 15000000 && x.price < 25000000);
                }
                else
                {
                    query = query.Where(x => x.price >= 25000000);
                }
            }
            if (index != 1)
            {
                result.Items = query.OrderByDescending(x => x.createdAt)
                .Skip(index).Take(8)
                .Select(x => new CatalogDTO()
                {
                    id = x.id,
                    name = x.name,
                    price = x.price,
                    pictureuri = x.pictureuri,
                }).ToList();
            }
            else
            {
                result.Items = query.OrderByDescending(x => x.createdAt)
                    .Skip(index - 1).Take(12)
                    .Select(x => new CatalogDTO()
                    {
                        id = x.id,
                        name = x.name,
                        price = x.price,
                        pictureuri = x.pictureuri,
                    }).ToList();
            }
            result.TotalRecord = result.Items.Count;
            return result;
        }

    }
}
