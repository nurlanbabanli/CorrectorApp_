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
    public class MbFieldDailyArchiveParameterDal : IFieldDailyArchiveParameterDal
    {
        public Task<List<FieldDailyArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            throw new NotImplementedException();
        }
    }
}
