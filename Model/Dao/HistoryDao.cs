using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class HistoryDao
    {
        OnlineShopDbContext _context = null;
        public HistoryDao()
        {
            _context = new OnlineShopDbContext();
        }
        public bool DeleteHistory(int id)
        {
            try
            {
                var history = _context.Histories.Find(id);
                _context.Histories.Remove(history);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool DeleteHistorys(List<int> ids)
        {
            var transction = _context.Database.BeginTransaction();
            try
            {
                foreach(var id in ids)
                {
                    var history = _context.Histories.Find(id);
                    _context.Histories.Remove(history);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                transction.Rollback();
                return false;
            }
            transction.Commit();
            return true;
        }
        public PagedResult<StatisticalDTO> GetAll(string seach, int brandid, int month, int page, int pageSize)
        {
            var query = from c in _context.Catalogs
                        join b in _context.CatalogBrands on c.catalogbrandid equals b.id
                        join h in _context.Histories on c.id equals h.catalogid
                        select new { c.name, b.id, b.brand, h };
            if (!string.IsNullOrEmpty(seach))
            {
                query = query.Where(x => x.name.Contains(seach));
            }
            if(brandid > 0)
            {
                query = query.Where(x => x.id == brandid);
            }
            if (month > 0)
            {
                query = query.Where(x => x.h.createdAt.Month == month
                && x.h.createdAt.Year == DateTime.Now.Year);
            }
            var result = new PagedResult<StatisticalDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderByDescending(x => x.h.createdAt)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new StatisticalDTO()
                {
                    id = x.h.id,
                    name = x.name,
                    brand = x.brand,
                    inputprice = x.h.inputprice,
                    unit = x.h.unit,
                    createdAt = x.h.createdAt.Day.ToString() +
                    "/" + x.h.createdAt.Month.ToString() +
                    "/" + x.h.createdAt.Year.ToString()
                }).ToList();
            return result;
        }
    }
}
