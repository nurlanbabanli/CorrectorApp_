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
    public class MbFieldCurrentParameterDal : IFieldCurrentParameterDal
    {
        public Task<List<FieldCurrentParameter>> GetFieldArchiveParametersAsync(CorrectorMaster deviceParameter)
        {
            throw new NotImplementedException();
        }
    }
}
