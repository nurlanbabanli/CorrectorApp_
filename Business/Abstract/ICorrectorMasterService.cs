using Core.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICorrectorMasterService
    {
        IDataResult<List<CorrectorMaster>> GetAll(Expression<Func<CorrectorMaster, bool>> expression = null);
        IDataResult<CorrectorMaster> Get(Expression<Func<CorrectorMaster, bool>> expression);
        IResult Update(CorrectorMaster correctorMaster);
        IResult Add(CorrectorMaster correctorMaster);
    }
}
