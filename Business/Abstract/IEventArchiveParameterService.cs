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
    public interface IEventArchiveParameterService
    {
        Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters);
        IResult AddArchiveParameterTransactionOperation(List<FieldEventArchiveParameter> fieldEventArchiveParameters);
        IDataResult<List<EventArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate);
    }
}
