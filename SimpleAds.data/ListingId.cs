using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAds.data
{
    public static class ListingId
    {
        public static int Id = 1;

        public static int Next()
        {
            return Id++;
        }
    }
}
