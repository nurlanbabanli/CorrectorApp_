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
    public class MbFieldMonthlyType1ArchiveParameterDal : IFieldMonthlyType1ArchiveParameterDal
    {
        public Task<List<FieldMonthlyType1ArchiveParameter>> GetFieldArchiveParametersAsync(CorrectorMaster deviceParameter)
        {
            throw new NotImplementedException();
        }
    }
}
