using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ZohoInvoiceClient
{
    static class Utils
    {
        internal static int ParseInt(string value)
        {
            int i = default(int);
            int.TryParse(value, out i);
            return i;
        }

        internal static long ParseLong(string value)
        {
            long l = default(long);
            long.TryParse(value, out l);
            return l;
        }

        internal static decimal ParseDecimal(string value)
        {
            decimal d = default(decimal);
            decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d);
            return d;
        }

        internal static DateTime ParseDate(string value)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            DateTime dt = default(DateTime);
            DateTime.TryParse(value, dtfi, DateTimeStyles.None, out dt);
            return dt;
        }
    }
}
