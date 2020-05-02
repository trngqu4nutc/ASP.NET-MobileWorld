using Model.DTO;
using Model.EF;
using Model.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BasketDao
    {
        OnlineShopDbContext _context = null;
        public BasketDao()
        {
            _context = new OnlineShopDbContext();
        }
        public List<BasketDTO> GetCatalogInCart(string username)
        {
            var query = from u in _context.Users
                        join b in _context.Baskets on u.id equals b.userid
                        join c in _context.Catalogs on b.catalogid equals c.id
                        where u.username == username
                        select new { b, c };
            return query.OrderBy(x => x.b.createdAt)
                .Select(x => new BasketDTO()
                {
                    userid = x.b.userid,
                    catalogid = x.b.catalogid,
                    catalogname = x.c.name,
                    pictureuri = x.c.pictureuri,
                    price = x.c.price,
                    unit = x.b.unit
                }).ToList();
        }
        public int AddToCart(int catalogid, string username)
        {
            var query = from u in _context.Users
                         join b in _context.Baskets on u.id equals b.userid
                         where u.username == username && b.userid == u.id && b.catalogid == catalogid
                         select new { b };
            var models = query.OrderBy(x => x.b.createdAt).Select(x => new BasketDTO()
            {
                userid = x.b.userid,
                catalogid = x.b.catalogid,
                unit = x.b.unit,
                createdAt = x.b.createdAt
            }).ToList();
            if (models.Count > 0)
            {
                var userid = models[0].userid;
                var basket = _context.Baskets.SingleOrDefault(x => x.userid == userid && x.catalogid == catalogid);
                basket = BasketMapper.toBasket(models[0], basket);
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
                var basket = new Basket();
                var userid = _context.Users.SingleOrDefault(x => x.username == username).id;
                basket = BasketMapper.toBasket(new BasketDTO() { catalogid = catalogid, userid = userid }, basket);
                _context.Baskets.Add(basket);
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
    }
}
