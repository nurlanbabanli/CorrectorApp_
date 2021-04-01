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
    public class MssqlEfDailyArchiveParameterDal:EfEntityRepositoryBase<DailyArchiveParameter,MssqlCorrectorContext>, IDailyArchiveParameterDal
    {
    }
}
