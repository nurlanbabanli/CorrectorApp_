using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tools
{
    public static class FormatDateTime
    {
        public static string FormatDateTimeValue(DateTime dateTime)
        {
           return dateTime.ToString("yyyy-MM-dd HH:mm:ss");         
        }
    }
}
