using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class StatisticalDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public decimal inputprice { get; set; }
        public int unit { get; set; }
        public string createdAt { get; set; }
    }
}
