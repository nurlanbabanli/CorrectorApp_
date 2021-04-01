using Core.Entities.Abstract;
using Core.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<TData> where TData : class, IEntity, new()
    {
        List<TData> GetAll(Expression<Func<TData, bool>> expression = null);
        TData Get(Expression<Func<TData, bool>> expression);
        void Add(TData entity);
        void Delete(TData entity);
    }
}
