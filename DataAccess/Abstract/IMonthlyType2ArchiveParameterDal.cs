using Core.DataAccess;
using Core.DataAccess.Mssql.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMonthlyType2ArchiveParameterDal: IEfEntityRepository<MonthlyType2ArchiveParameter>
    {
    }
}
