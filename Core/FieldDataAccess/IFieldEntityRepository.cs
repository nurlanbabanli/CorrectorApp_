using Core.ActionReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FieldDataAccess
{
    public interface IFieldEntityRepository<TResultData, TDeviceParameter>
    {
        Task<List<TResultData>> GetFieldArchiveParametersAsync(TDeviceParameter deviceParameter,IProgress<ProgressStatus> progress);
    }
}
