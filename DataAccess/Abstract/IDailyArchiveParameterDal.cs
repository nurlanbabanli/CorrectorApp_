using Core.DataAccess;
using Core.DataAccess.Mssql.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDailyArchiveParameterDal:IEfEntityRepository<DailyArchiveParameter>
    {
    }
}
