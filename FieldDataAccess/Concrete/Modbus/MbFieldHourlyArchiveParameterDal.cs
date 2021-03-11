using Core.ActionReports;
using Core.Utilities.FieldDeviceIdentifier;
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

        public Task<List<FieldHourlyArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            _result = new List<FieldHourlyArchiveParameter>();
            return (Task<List<FieldHourlyArchiveParameter>>)Task.Run(() =>
            {
                // below codes is for test. real codes will be added later.
                
                for (int i = 0; i < 100; i++)
                {
                    _fieldHourlyParameter = new FieldHourlyArchiveParameter();
                    _fieldHourlyParameter.ABNo = deviceParameter.DeviceParametersHolder.Id * 10 + i;
                    _fieldHourlyParameter.DeviceId = deviceParameter.DeviceParametersHolder.Id;

                    deviceParameter.UserInterfaceParametersHolder.ProgressReport.Report(new ProgressStatus { Progress = i });
                    _result.Add(_fieldHourlyParameter);
                    for (int k = 0; k <10000; k++)
                    {
                        for (int l = 0; l < 1000; l++)
                        {
                            int g = (k + l) * l;
                        }                      
                    }
                    
                }

                deviceParameter.SemaphoreSlimT.Release();
                return _result;
            });

            
        }
    }
}
