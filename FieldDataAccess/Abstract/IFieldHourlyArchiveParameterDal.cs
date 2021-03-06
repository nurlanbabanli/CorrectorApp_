using Core.FieldDataAccess;
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
    public interface IFieldHourlyArchiveParameterDal : IFieldEntityRepository<FieldHourlyArchiveParameter, DataTransmissionParameterHolder>
    {

    }
}
