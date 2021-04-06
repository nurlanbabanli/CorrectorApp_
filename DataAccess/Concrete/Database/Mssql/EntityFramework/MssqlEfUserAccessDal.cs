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
    public class MssqlEfUserAccessDal : IUserAccessDal
    {
        public void Add(UserAccess userAccess)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var addedEntity = context.Entry(userAccess);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(UserAccess userAccess)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var addedEntity = context.Remove(userAccess);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public UserAccess Get(Expression<Func<UserAccess, bool>> expression)
        {
            using (var context = new MssqlCorrectorContext())
            {
                return context.Set<UserAccess>().SingleOrDefault(expression);
            }
        }

        public List<UserAccess> GetAll(Expression<Func<UserAccess, bool>> expression = null)
        {
            using (var context = new MssqlCorrectorContext())
            {
                return expression == null
                    ? context.Set<UserAccess>().ToList()
                    : context.Set<UserAccess>().Where(expression).ToList();
            }
        }

        public void Update(UserAccess userAccess)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var updatedEntity = context.Entry(userAccess);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
