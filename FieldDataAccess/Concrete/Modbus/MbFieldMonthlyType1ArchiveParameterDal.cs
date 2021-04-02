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
    public class MbFieldMonthlyType1ArchiveParameterDal : IFieldMonthlyType1ArchiveParameterDal
    {
        public MbFieldMonthlyType1ArchiveParameterDal()
        {

        }
        public Task<List<FieldMonthlyType1ArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {
            return (Task<List<FieldMonthlyType1ArchiveParameter>>)Task.Run(() =>
            {
                List<FieldMonthlyType1ArchiveParameter> fieldMonthlyType1ArchiveParameters = new List<FieldMonthlyType1ArchiveParameter>();
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
                return fieldMonthlyType1ArchiveParameters;
            });
        }
    }
}
