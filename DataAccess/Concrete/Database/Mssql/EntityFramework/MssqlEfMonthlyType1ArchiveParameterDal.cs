using Core.DataAccess.Mssql.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Database.Mssql.EntityFramework
{
    public class MssqlEfMonthlyType1ArchiveParameterDal:EfEntityRepositoryBase<MonthlyType1ArchiveParameter,MssqlCorrectorContext>, IMonthlyType1ArchiveParameterDal
    {
    }
}
