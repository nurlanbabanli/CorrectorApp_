using Core.ActionReports;
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
        Task<IResult> GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters, IProgress<ProgressStatus> progress);
        IResult AddArchiveParameterTransactionOperation(List<FieldDailyArchiveParameter> fieldDailyArchiveParameters, IProgress<ProgressStatus> progress);
        IDataResult<List<DailyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate);
    }
}
