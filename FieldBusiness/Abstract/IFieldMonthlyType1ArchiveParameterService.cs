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
    public interface IFieldMonthlyType1ArchiveParameterService : IResultEvent<FieldMonthlyType1ArchiveParameter, IProgress<ProgressStatus>>
    {
        Task GetMonthlyType1ArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter);
    }
}
