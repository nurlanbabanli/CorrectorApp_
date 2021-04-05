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
    public class MssqlEfUserAccessTypeDal : IUserAccessTypeDal
    {
        public List<UserAccessType> GetAll(Expression<Func<UserAccessType, bool>> expression = null)
        {
            using (var context = new MssqlCorrectorContext())
            {
                return expression == null
                    ? context.Set<UserAccessType>().ToList()
                    : context.Set<UserAccessType>().Where(expression).ToList();
            }
        }
    }
}
