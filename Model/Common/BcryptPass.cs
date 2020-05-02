using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Common
{
    public static class BcryptPass
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}