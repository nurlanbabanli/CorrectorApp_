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
    public class MbFieldMonthlyType2ArchiveParameterDal : IFieldMonthlyType2ArchiveParameterDal
    {
        IFieldMonthlyType2ArchivePart1ParameterDal _fieldMonthlyType2ArchivePart1ParameterDal;
        IFieldMonthlyType2ArchivePart2ParameterDal _fieldMonthlyType2ArchivePart2ParameterDal;
        IFieldMonthlyType2ArchivePart3ParameterDal _fieldMonthlyType2ArchivePart3ParameterDal;
        public MbFieldMonthlyType2ArchiveParameterDal()
        {
            _fieldMonthlyType2ArchivePart1ParameterDal = new MbFieldMonthlyType2ArchivePart1PArameterDal();
            _fieldMonthlyType2ArchivePart2ParameterDal = new MbFieldMonthlyType2ArchivePart2PArameterDal();
            _fieldMonthlyType2ArchivePart3ParameterDal = new MbFieldMonthlyType2ArchivePart3PArameterDal();
        }
        public Task<List<FieldMonthlyType2ArchiveParameter>> GetFieldArchiveParametersAsync(DataTransmissionParameterHolder deviceParameter)
        {

            return (Task<List<FieldMonthlyType2ArchiveParameter>>)Task.Run(() =>
            {
                List<FieldMonthlyType2ArchiveParameter> fieldMonthlyType2ArchiveParameters = new List<FieldMonthlyType2ArchiveParameter>();
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
                return fieldMonthlyType2ArchiveParameters;
            });
        }
    }
}
