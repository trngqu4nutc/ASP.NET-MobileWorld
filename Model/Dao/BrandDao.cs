using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BrandDao
    {
        OnlineShopDbContext _context = null;

        public BrandDao()
        {
            _context = new OnlineShopDbContext();
        }
        public PagedResult<BrandDTO> GetListBrand(string brand, int page, int pageSize)
        {
            IQueryable<CatalogBrand> query = _context.CatalogBrands;
            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(x => x.brand.Contains(brand));
            }
            PagedResult<BrandDTO> result = new PagedResult<BrandDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderBy(x => x.id).Skip((page - 1) * pageSize)
                .Take(pageSize).Select(x => new BrandDTO()
                {
                    id = x.id,
                    brand = x.brand,
                    pictureurl = x.pictureurl
                }).ToList();
            return result;
        }
        public int SaveBrand(BrandDTO model)
        {
            if (model.id == 0)
            {
                var data = _context.CatalogBrands.Count(x => x.brand == model.brand);
                if (data > 0)
                {
                    return -1;
                }
                var brand = new CatalogBrand();
                brand.brand = model.brand;
                brand.pictureurl = model.pictureurl;
                _context.CatalogBrands.Add(brand);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
            {
                var brand = _context.CatalogBrands.Find(model.id);
                brand.brand = model.brand;
                brand.pictureurl = model.pictureurl;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 1;
        }
        public int DeleteBrand(int id)
        {
            var brand = _context.CatalogBrands.Find(id);
            _context.CatalogBrands.Remove(brand);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
        public BrandDTO LoadDetail(int id)
        {
            var data = _context.CatalogBrands.Find(id);
            var result = new BrandDTO();
            result.id = data.id;
            result.brand = data.brand;
            result.pictureurl = data.pictureurl;
            return result;
        }
        public List<BrandDTO> GetBrand(int type)
        {
            var query = from c in _context.Catalogs
                        join b in _context.CatalogBrands on c.catalogbrandid equals b.id
                        where c.catalogtypeid == type
                        select new { b.id, b.pictureurl };
            var result = new List<BrandDTO>();
            foreach(var item in query)
            {
                if(check(item.id, result))
                {
                    var brand = new BrandDTO();
                    brand.id = item.id;
                    brand.pictureurl = item.pictureurl;
                    result.Add(brand);
                }
            }
            return result;
        }
        private bool check(int id, List<BrandDTO> brands)
        {
            foreach (var item in brands)
            {
                if (item.id == id)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
