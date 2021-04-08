using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Database.Mssql.EntityFramework
{
    public class MssqlEfUserLogDal : IUserLogDal
    {
        public void Add(UserLog userLog)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var addedEntity = context.Entry(userLog);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public UserLog Get(Expression<Func<UserLog, bool>> expression)
        {
            using (var context = new MssqlCorrectorContext())
            {                
                return context.Set<UserLog>().SingleOrDefault(expression);
            }
        }

        public void Update(UserLog userLog)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var updatedEntity = context.Entry(userLog);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
