using Core.ActionReports;
using Core.Utilities.DeviceIdentifier;
using Entities.Concrete;
using FieldDataAccess.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FieldDataAccess.Concrete.Modbus
{
    public class MbFieldHourlyArchiveParameterDal : IFieldHourlyArchiveParameterDal
    {
        List<FieldHourlyArchiveParameter> _result;
        FieldHourlyArchiveParameter _fieldHourlyParameter;

        public MbFieldHourlyArchiveParameterDal()
        {

        }

        public Task<List<FieldHourlyArchiveParameter>> GetFieldArchiveParametersAsync(IDeviceParameter deviceParameter, IProgress<ProgressStatus> progress)
        {
            _result = new List<FieldHourlyArchiveParameter>();

            return (Task<List<FieldHourlyArchiveParameter>>)Task.Run(() =>
            {
                // below codes is for test. real codes will be added later.
                for (int i = 0; i < 20; i++)
                {
                    _fieldHourlyParameter = new FieldHourlyArchiveParameter();
                    _fieldHourlyParameter.ABNo = correctorMaster.Id * 10 + i;
                    _fieldHourlyParameter.DeviceId = correctorMaster.Id;

                    progress.Report(new ProgressStatus { Progress = i });
                    _result.Add(_fieldHourlyParameter);
                    Thread.Sleep(20);
                }

                return _result;
            });
        }
    }
}
