using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldDataAccess.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldDataAccess.Concrete.Modbus
{
    public class MbFieldEventArchiveParameterDal : IFieldEventArchiveParameterDal
    {
        public Task<List<FieldEventArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            throw new NotImplementedException();
        }
    }
}
