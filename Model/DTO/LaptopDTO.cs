using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class LaptopDTO : CatalogDTO
    {
        public string cardscreen { get; set; }
        public string connector { get; set; }
        public string harddrive { get; set; }
        public string design { get; set; }
        public string size { get; set; }
        public string release { get; set; }
    }
}
