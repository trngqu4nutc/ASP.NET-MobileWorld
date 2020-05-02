using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Util
{
    public static class BasketMapper
    {
        public static Basket toBasket(BasketDTO model, Basket basket)
        {
            basket.userid = model.userid;
            basket.catalogid = model.catalogid;
            if(basket.unit != 0)
            {
                basket.unit += 1;
                basket.updatedAt = DateTime.Now;
            }
            else
            {
                basket.unit = 1;
                basket.createdAt = DateTime.Now;
                basket.updatedAt = DateTime.Now;
            }
            return basket;
        }
    }
}
