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
    public interface IMonthlyType2ArchiveParameterService
    {
        Task<IResult> GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters, IProgress<ProgressStatus> progress);
        IResult AddArchiveParameterTransactionOperation(List<FieldMonthlyType2ArchiveParameter> fieldMonthlyType2ArchiveParameters, IProgress<ProgressStatus> progress);
        IDataResult<List<MonthlyType2ArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate);
    }
}
