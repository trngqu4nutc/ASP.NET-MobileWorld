using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class MobileDTO : CatalogDTO
    {
        public string backcamera { get; set; }
        public string frontcamera { get; set; }
        public string internalmemory { get; set; }
        public string memorystick { get; set; }
        public string sim { get; set; }
        public string batery { get; set; }

    }
}
