using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public static class GeneratorPassword
    {
        public static string Password()
        {
            var password = new StringBuilder();
            var possible = new StringBuilder("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
            var random = new Random();
            while (true)
            {
                int i = Convert.ToInt32(random.NextDouble()*100);
                if (i < 62)
                {
                    password.Append(possible[i]);
                    if(password.Length == 10)
                    {
                        break;
                    }
                }
            }
            return password.ToString();
        }
    }
}
