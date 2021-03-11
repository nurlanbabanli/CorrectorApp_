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
    public interface IFieldMonthlyType2ArchivePart3ParameterDal
    {
        FieldMonthlyType2ArchivePart3Parameter GetFieldMonthlyType2ArchivePart3Parameter(DataTransmissionParameterHolder deviceParameter);
    }
}
