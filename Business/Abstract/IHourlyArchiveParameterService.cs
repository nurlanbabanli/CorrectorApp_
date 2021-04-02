using Autofac;
using Core.Results.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHourlyArchiveParameterService
    {
        Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters);
        IResult AddArchiveParameterTransactionOperation(List<FieldHourlyArchiveParameter> hourlyArchiveParameters);
        IDataResult<List<HourlyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId,DateTime beginDate, DateTime endDate);
    }
}
