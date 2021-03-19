using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FieldDeviceIdentifier
{
    public class DataTransmissionParametersHolderList
    {
        public List<DataTransmissionParameterHolder> DataTransmissionParameterHolderList { get; set; }

        public DataTransmissionParametersHolderList()
        {
            DataTransmissionParameterHolderList = new List<DataTransmissionParameterHolder>();
        }
    }
}
