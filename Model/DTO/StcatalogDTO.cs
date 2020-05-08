using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class StcatalogDTO
    {
        public int id { get; set; }
        public int catalogid { get; set; }
        public int brandid { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public int unit { get; set; }
        public int quantity { get; set; }
        public decimal cost { get; set; }
        public DateTimeOffset createdAt { get; set; }
    }
}
