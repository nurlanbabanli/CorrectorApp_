using Core.ActionReports;
using Core.Events.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Abstract
{
    public interface IFieldDailyArchiveParameterService : IResultEvent<FieldDailyArchiveParameter,IProgress<ProgressStatus>>
    {
        Task GetArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter);
    }
}
