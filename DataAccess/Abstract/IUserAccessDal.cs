using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserAccessDal
    {
        List<UserAccess> GetAll(Expression<Func<UserAccess, bool>> expression = null);
        UserAccess Get(Expression<Func<UserAccess, bool>> expression);
        void Add(UserAccess userAccess);
        void Update(UserAccess userAccess);
        void Delete(UserAccess userAccess);
    }
}
