using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public bool status { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int role { get; set; }

    }
}
