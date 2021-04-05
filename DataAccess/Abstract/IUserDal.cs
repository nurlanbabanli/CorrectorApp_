using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        List<User> GetAll(Expression<Func<User, bool>> expression=null);
        User Get(Expression<Func<User, bool>> expression);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
