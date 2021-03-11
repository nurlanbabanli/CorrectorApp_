using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldDataAccess.Abstract
{
    public interface IFieldMonthlyType2ArchivePart1ParameterDal
    {
        FieldMonthlyType2ArchivePart1Parameter GetFieldMonthlyType2ArchivePart1Parameter(DataTransmissionParameterHolder deviceParameter);
    }
}
