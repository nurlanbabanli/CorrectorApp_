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
    public class MbFieldCurrentParameterDal : IFieldCurrentParameterDal
    {
        public MbFieldCurrentParameterDal()
        {

        }
        public Task<List<FieldCurrentParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            return (Task<List<FieldCurrentParameter>>)Task.Run(() =>
            {
                List<FieldCurrentParameter> fieldCurrentParameters = new List<FieldCurrentParameter>();
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
                return fieldCurrentParameters;
            });
        }
    }
}
