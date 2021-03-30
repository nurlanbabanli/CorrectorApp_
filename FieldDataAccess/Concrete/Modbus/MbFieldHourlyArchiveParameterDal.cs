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
        
        FieldHourlyArchiveParameter _fieldHourlyParameter;

        public MbFieldHourlyArchiveParameterDal()
        {

        }

        public Task<List<FieldHourlyArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldHourlyArchiveParameter> result = new List<FieldHourlyArchiveParameter>();

            return (Task<List<FieldHourlyArchiveParameter>>)Task.Run(() =>
            {
                // below codes is for test. real codes will be added later.

                try
                {
                    result = MyTestMethod1(deviceParameter);
                }
                catch (Exception ex)
                {
                    string exc = ex.Message;
                }
                finally
                {
                    deviceParameter.SemaphoreSlimT.Release();
                }
                return result;
            });
        }

        private List<FieldHourlyArchiveParameter> MyTestMethod1(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldHourlyArchiveParameter> result = new List<FieldHourlyArchiveParameter>();
            for (int i = 0; i < 100; i++)
            {
                _fieldHourlyParameter = new FieldHourlyArchiveParameter();
                _fieldHourlyParameter.ABNo = deviceParameter.DeviceParametersHolder.Id * 10 + i;
                _fieldHourlyParameter.DeviceId = deviceParameter.DeviceParametersHolder.Id;

                deviceParameter.UserInterfaceParametersHolder.ProgressReport.Report(new ProgressStatus { Progress = i });
                result.Add(_fieldHourlyParameter);
                for (int k = 0; k < 10000; k++)
                {
                    for (int l = 0; l < 100; l++)
                    {
                        int g = (k + l) * l;
                    }
                   
                }
                 Thread.Sleep(2);
            }
            return result;
        }

    }
}
