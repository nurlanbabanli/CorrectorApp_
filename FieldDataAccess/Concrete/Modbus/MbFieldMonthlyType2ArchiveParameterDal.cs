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
        List<FieldMonthlyType2ArchiveParameter> _result;
        FieldMonthlyType2ArchiveParameter _fieldMonthlyType2ArchiveParameter;
        public MbFieldMonthlyType2ArchiveParameterDal()
        {
            _fieldMonthlyType2ArchivePart1ParameterDal = new MbFieldMonthlyType2ArchivePart1PArameterDal();
            _fieldMonthlyType2ArchivePart2ParameterDal = new MbFieldMonthlyType2ArchivePart2PArameterDal();
            _fieldMonthlyType2ArchivePart3ParameterDal = new MbFieldMonthlyType2ArchivePart3PArameterDal();

            _result = new List<FieldMonthlyType2ArchiveParameter>();
            _fieldMonthlyType2ArchiveParameter = new FieldMonthlyType2ArchiveParameter();
        }
        public Task<List<FieldMonthlyType2ArchiveParameter>> GetFieldArchiveParametersAsync(CorrectorMaster deviceParameter)
        {

            return (Task<List<FieldMonthlyType2ArchiveParameter>>)Task.Run(() =>
            {

                return _result;
            });
        }
    }
}
