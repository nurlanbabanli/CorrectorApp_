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
    public interface IMonthlyType1ArchiveParameterService
    {
        Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters);
        IResult AddArchiveParameterTransactionOperation(List<FieldMonthlyType1ArchiveParameter> fieldMonthlyType1ArchiveParameters);
        IDataResult<List<MonthlyType1ArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate);
    }
}
