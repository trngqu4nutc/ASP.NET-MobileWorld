using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BillDao
    {
        OnlineShopDbContext _context = null;
        public BillDao()
        {
            _context = new OnlineShopDbContext();
        }
        public bool UpdateBill(int id, int status)
        {
            var bill = _context.Bills.Find(id);
            bill.status = status;
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
        public bool DeleteBill(int id)
        {
            var bill = _context.Bills.Find(id);
            _context.Bills.Remove(bill);
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
        public PagedResult<BillDTO> LoadData(string seach, int status, int page, int pageSize)
        {
            var query = from b in _context.Bills
                        join u in _context.Users on b.userid equals u.id
                        join ur in _context.UserRoles on u.id equals ur.userid
                        where ur.roleid == 1
                        select new { b, u.username };
            if (!string.IsNullOrEmpty(seach))
            {
                query = query.Where(x => x.username.Contains(seach));
            }
            if(status != -2)
            {
                query = query.Where(x => x.b.status == status);
            }
            var result = new PagedResult<BillDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderByDescending(x => x.b.createdAt)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new BillDTO()
                {
                    id = x.b.id,
                    username = x.username,
                    name = x.b.name,
                    pictureuri = x.b.pictureuri,
                    price = x.b.price,
                    unit = x.b.unit,
                    status = x.b.status
                }).ToList();
            return result;
        }
    }
}
