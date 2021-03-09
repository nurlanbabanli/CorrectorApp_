using Core.FieldDataAccess;
using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldDataAccess.Abstract
{
    public interface IFieldMonthlyType1ArchiveParameterDal : IFieldEntityRepository<FieldMonthlyType1ArchiveParameter, CorrectorMaster>
    {
    }
}
