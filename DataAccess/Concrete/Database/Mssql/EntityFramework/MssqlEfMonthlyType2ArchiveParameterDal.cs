using Core.DataAccess.Mssql.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Database.Mssql.EntityFramework
{
    public class MssqlEfMonthlyType2ArchiveParameterDal: EfEntityRepositoryBase<MonthlyType2ArchiveParameter,MssqlCorrectorContext>, IMonthlyType2ArchiveParameterDal
    {
    }
}
