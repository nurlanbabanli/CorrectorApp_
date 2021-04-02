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
    public class MbFieldEventArchiveParameterDal : IFieldEventArchiveParameterDal
    {
        public MbFieldEventArchiveParameterDal()
        {

        }
        public Task<List<FieldEventArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            
            return (Task<List<FieldEventArchiveParameter>>)Task.Run(() =>
            {               
                List<FieldEventArchiveParameter> fieldEventArchiveParameters = new List<FieldEventArchiveParameter>();
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
                return fieldEventArchiveParameters;
            });
        }
    }
}
