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
    public interface IFieldMonthlyType2ArchivePart2ParameterDal
    {
        FieldMonthlyType2ArchivePart2Parameter GetFieldMonthlyType2ArchivePart2Parameter(DataTransmissionParameterHolder deviceParameter);
    }
}
