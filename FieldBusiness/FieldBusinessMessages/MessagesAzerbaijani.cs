using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.FieldBusinessMessages
{
    public class MessagesAzerbaijani
    {
        public static string DeviceIpIsNull { get { return "Qurğunun Ip-i boş olmamalıdır. "; } }
        public static string DeviceIdIsNull { get { return "Qurğunun Id-i boş olmamalıdır. "; } }
        public static string DeviceTypeIsNull { get { return "Qurğunun tipi boş olmamalıdır. "; } }
        public static string DeviceIsNotActive { get { return "Qurğunu aktiv rejimdə olmalıdır. "; } }
        public static string DeviceParametersHolderIsNull { get { return "Qurğu parameterləri boş olmamalıdır. "; } }
        public static string SemaphoreSlimTIsNull { get { return "SemaphoreSlim boş olmamalıdır. "; } }
    }
}
