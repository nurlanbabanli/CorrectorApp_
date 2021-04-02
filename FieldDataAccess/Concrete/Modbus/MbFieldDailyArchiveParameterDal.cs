using Core.ActionReports;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldDataAccess.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldDataAccess.Concrete.Modbus
{
    public class MbFieldDailyArchiveParameterDal : IFieldDailyArchiveParameterDal
    {
        public MbFieldDailyArchiveParameterDal()
        {

        }
        public Task<List<FieldDailyArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            return (Task<List<FieldDailyArchiveParameter>>)Task.Run(() =>
            {
                List<FieldDailyArchiveParameter> fieldDailyArchiveParameters = new List<FieldDailyArchiveParameter>();
                try
                {
                    // codes will be added later.
                }
                catch (Exception ex)
                {
                    deviceParameter.UserInterfaceParametersHolder.ProgressReport.Report(new ProgressStatus { StatusId = MessageStatus.Error, Message = ex.Message });
                }
                finally
                {
                    deviceParameter.SemaphoreSlimT.Release();
                }
                return fieldDailyArchiveParameters;
            });
        }
    }
}
