using Core.Results.Abstract;
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
    public class MssqlEfCorrectorMasterDal : ICorrectorMasterDal
    {
        public void Add(CorrectorMaster correctorMaster)
        {
            using (var context=new MssqlCorrectorContext() )
            {
                var addedEntity = context.Entry(correctorMaster);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(CorrectorMaster correctorMaster)
        {
            throw new NotImplementedException();
        }

        public CorrectorMaster Get(Expression<Func<CorrectorMaster, bool>> expression)
        {
            using (var context=new MssqlCorrectorContext())
            {
                return context.Set<CorrectorMaster>().SingleOrDefault(expression);
            }
        }

        public List<CorrectorMaster> GetAll(Expression<Func<CorrectorMaster, bool>> expression = null)
        {
            using (var context=new MssqlCorrectorContext())
            {
                return expression == null
                    ? context.Set<CorrectorMaster>().ToList()
                    : context.Set<CorrectorMaster>().Where(expression).ToList();
            }
        }

        public void Update(CorrectorMaster correctorMaster)
        {
            using (var context=new MssqlCorrectorContext())
            {
                var updatedEntity = context.Entry(correctorMaster);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
