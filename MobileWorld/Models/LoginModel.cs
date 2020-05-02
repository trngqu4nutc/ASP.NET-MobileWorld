using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileWorld.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    }
}