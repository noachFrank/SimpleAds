using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAds.data
{
    public static class ReaderExtensions
    {
        public static T GetOrNull<T>(this SqlDataReader reader, string name)
        {
            object value = reader[name];
            if(value == DBNull.Value)
            {
                return default(T);
            }
            return (T)value;
        }
    }
}
