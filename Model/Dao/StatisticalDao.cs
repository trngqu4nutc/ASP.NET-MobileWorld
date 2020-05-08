using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class StatisticalDao
    {
        OnlineShopDbContext _context = null;
        public StatisticalDao()
        {
            _context = new OnlineShopDbContext();
        }
        public PagedResult<StcatalogDTO> GetAllCatalog(string seach, int brandid, int month, int page, int pageSize)
        {
            var listResult = new List<StcatalogDTO>();
            if (month > 0)
            {
                listResult = Filter(month);
            }
            else
            {
                listResult = Filter();
            }
            if (!string.IsNullOrEmpty(seach))
            {
                listResult = listResult.Where(x => x.name.Contains(seach)).ToList();
            }
            if(brandid > 0)
            {
                listResult = listResult.Where(x => x.brandid == brandid).ToList();
            }
            var result = new PagedResult<StcatalogDTO>();
            result.TotalRecord = listResult.Count;
            result.Total = Convert.ToDouble(listResult.Sum(x => x.cost));
            result.Items = listResult.OrderByDescending(x => x.createdAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }
        private List<StcatalogDTO> Filter()
        {
            var query = from c in _context.Catalogs
                        join br in _context.CatalogBrands on c.catalogbrandid equals br.id
                        join b in _context.Bills on c.id equals b.catalogid
                        where b.status == 2
                        select new
                        {
                            b.catalogid,
                            b.id,
                            brandid = br.id,
                            b.name,
                            br.brand,
                            b.unit,
                            c.quantity,
                            c.price,
                            b.createdAt
                        } into t1
                        group t1 by t1.catalogid into cb
                        select new
                        {
                            catalogid = cb.Key,
                            model = cb.Select(x => new StcatalogDTO()
                            {
                                id = x.id,
                                brandid = x.brandid,
                                catalogid = x.catalogid,
                                name = x.name,
                                brand = x.brand,
                                quantity = x.quantity,
                                cost = x.price,
                                createdAt = x.createdAt
                            }),
                            count = cb.Sum(x => x.unit)
                        };
            var result = new List<StcatalogDTO>();
            foreach (var item in query)
            {
                int i = item.model.Count() - 1;
                var element = item.model.ToList()[i];
                var inputprice = _context.Histories.FirstOrDefault(x => x.catalogid == item.catalogid).inputprice;
                element.unit = item.count;
                element.cost = (element.cost - inputprice) * element.unit;
                result.Add(element);
            }
            return result;
        }
        private List<StcatalogDTO> Filter(int month)
        {
            var query = from c in _context.Catalogs
                        join br in _context.CatalogBrands on c.catalogbrandid equals br.id
                        join b in _context.Bills on c.id equals b.catalogid
                        where b.status == 2 && b.createdAt.Month == month
                        select new
                        {
                            b.catalogid,
                            b.id,
                            brandid = br.id,
                            b.name,
                            br.brand,
                            b.unit,
                            c.quantity,
                            c.price,
                            b.createdAt
                        } into t1
                        group t1 by t1.catalogid into cb
                        select new
                        {
                            catalogid = cb.Key,
                            model = cb.Select(x => new StcatalogDTO()
                            {
                                id = x.id,
                                catalogid = x.catalogid,
                                brandid = x.brandid,
                                name = x.name,
                                brand = x.brand,
                                quantity = x.quantity,
                                cost = x.price,
                                createdAt = x.createdAt
                            }),
                            count = cb.Sum(x => x.unit)
                        };
            var result = new List<StcatalogDTO>();
            foreach (var item in query)
            {
                int i = item.model.Count() - 1;
                var element = item.model.ToList()[i];
                var inputprice = _context.Histories.FirstOrDefault(x => x.catalogid == item.catalogid).inputprice;
                element.unit = item.count;
                element.cost = (element.cost - inputprice) * element.unit;
                result.Add(element);
            }
            return result;
        }
    }
}
