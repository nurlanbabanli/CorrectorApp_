using Core.DataAccess.Mssql.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Database.Mssql.EntityFramework
{
    public class MssqlEfUserAccessTypeDal :EfEntityRepositoryBase<UserAccessType,MssqlCorrectorContext>, IUserAccessTypeDal
    {

    }
}
