using Business.Abstract;
using Core.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CorrectorMasterManager : ICorrectorMasterService
    {
        ICorrectorMasterDal _correctorMasterDal;

        public CorrectorMasterManager(ICorrectorMasterDal correctorMasterDal)
        {
            _correctorMasterDal = correctorMasterDal;
        }

        public IResult Add(CorrectorMaster correctorMaster)
        {
            throw new NotImplementedException();
        }

        public IDataResult<CorrectorMaster> Get(Expression<Func<CorrectorMaster, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CorrectorMaster>> GetAll(Expression<Func<CorrectorMaster, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public IResult Update(CorrectorMaster correctorMaster)
        {
            throw new NotImplementedException();
        }
    }
}
