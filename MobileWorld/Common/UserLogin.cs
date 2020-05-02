using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileWorld.Common
{
    [Serializable]
    public class UserLogin
    {
        public int userid { get; set; }
        public string username { get; set; }
        public int role { get; set; }
    }
}