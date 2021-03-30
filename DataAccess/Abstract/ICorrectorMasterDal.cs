using Core.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICorrectorMasterDal
    {
        List<CorrectorMaster> GetAll(Expression<Func<CorrectorMaster, bool>> expression = null);
        CorrectorMaster Get(Expression<Func<CorrectorMaster, bool>> expression);
        void Add(CorrectorMaster correctorMaster);
        void Update(CorrectorMaster correctorMaster);
        void Delete(CorrectorMaster correctorMaster);
    }
}
