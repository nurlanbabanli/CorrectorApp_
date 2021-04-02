using Core.Results.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDailyArchiveParameterService
    {
        Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters);
        IResult AddArchiveParameterTransactionOperation(List<FieldDailyArchiveParameter> fieldDailyArchiveParameters);
        IDataResult<List<DailyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate);
    }
}
