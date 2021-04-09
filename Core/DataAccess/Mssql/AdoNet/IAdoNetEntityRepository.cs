using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Mssql.AdoNet
{
    public interface IAdoNetEntityRepository<TData> where TData : class, IEntity, new()
    {
        void Add(TData entity);
        void Delete(TData entity);
        void Update(TData entity);
    }
}
