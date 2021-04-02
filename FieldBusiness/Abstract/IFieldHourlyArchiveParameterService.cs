using Core.ActionReports;
using Core.Events.Abstract;
using Core.Results.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Abstract
{
    public interface IFieldHourlyArchiveParameterService : IResultEvent<FieldHourlyArchiveParameter,IProgress<ProgressStatus>>
    {
        Task GetHourArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter);
    }
}
