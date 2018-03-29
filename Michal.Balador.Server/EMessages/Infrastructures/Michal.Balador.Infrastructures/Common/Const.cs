using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lior.api.Helper
{
    public static class General
    {
        public const int MaxRecordsPerSearch = 100;
        public const int MaxRecordsPerPage = 10;
        public const string OrgWWW = "www";
        public const string OrgIDWWW = "00000000-0000-0000-0000-000000000002";

        public const string UnitMin = "דק'";
        public const string Unit = "יח'";

        public const string Shekel = "₪";

        public const string Empty = "---";

        public const double MAXMinutesExpiredApiToken = 60 * 60 ;

        public const string NgAutoApp = "ngAutoApp";
        public static class MessageStatus
        {
            public const int Pending = 0;
        }

    }

}