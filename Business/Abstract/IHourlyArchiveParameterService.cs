using Autofac;
using Core.ActionReports;
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
        Task<IResult> GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters, IProgress<ProgressStatus> progress);
        IResult AddArchiveParameterTransactionOperation(List<FieldHourlyArchiveParameter> hourlyArchiveParameters, IProgress<ProgressStatus> progress);
        IDataResult<List<HourlyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId,DateTime beginDate, DateTime endDate);
    }
}
