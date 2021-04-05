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
    public class MssqlEfUserDal : IUserDal
    {
        public void Add(User user)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var addedEntity = context.Entry(user);
                //addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            using (var context = new MssqlCorrectorContext())
            {
                return context.Set<User>().SingleOrDefault(expression);
            }
        }

        public List<User> GetAll(Expression<Func<User, bool>> expression = null)
        {
            using (var context = new MssqlCorrectorContext())
            {
                return expression == null
                    ? context.Set<User>().ToList()
                    : context.Set<User>().Where(expression).ToList();
            }
        }

        public void Update(User user)
        {
            using (var context = new MssqlCorrectorContext())
            {
                var updatedEntity = context.Entry(user);
                //updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
