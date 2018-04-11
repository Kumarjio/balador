using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Util
{
    public static class SExstenstion
    {
        static string[] Helper(string name)
        {
            return name.Split('%');
        }
        public static string GetMessaggerShrotName(this string name)
        {
            var aa= Helper(name);
            if (aa != null && aa.Any() && aa.Count() > 1)
                return aa[1];
            return name;

        }
        public static string GetMessaggerDomainName(this string name)
        {
            var aa = Helper(name);
            if (aa != null && aa.Any() && aa.Count() > 1)
                return aa[0];
            return name;
        }
    }
}
