using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class BillDTO
    {
        public int id { get; set; }
        public decimal price { get; set; }
        public int unit { get; set; }
        public string pictureuri { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string username { get; set; }
    }
}
