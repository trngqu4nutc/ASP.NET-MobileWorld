using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CatalogDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pictureuri { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public int quantity { get; set; }
        public bool status { get; set; }
        public string brand { get; set; }
        public string type { get; set; }
        public string cpu { get; set; }
        public string ram { get; set; }
        public string screen { get; set; }
        public string os { get; set; }
        public int catalogtypeid { get; set; }
        public int catalogbrandid { get; set; }
    }
}
