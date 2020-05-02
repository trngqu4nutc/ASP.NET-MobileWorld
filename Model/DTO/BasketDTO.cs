using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class BasketDTO
    {
        public int userid { get; set; }
        public int catalogid { get; set; }
        public string catalogname { get; set; }
        public string pictureuri { get; set; }
        public decimal price { get; set; }
        public int unit { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updatedAt { get; set; }
    }
}
