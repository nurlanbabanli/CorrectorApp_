using Core.Utilities.InMemoryLoggerParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.Logging
{
    public class MessageFormatter
    {
        public static string GetMessage(List<InMemoryLoggerParameter> inMemoryLoggerParameters, int id)
        {
            foreach (var item in inMemoryLoggerParameters)
            {
                if (item.DeviceId == id || item.DeviceId==0)
                {
                    return item.Messages;
                }
            }
            return null;
        }
    }
}
