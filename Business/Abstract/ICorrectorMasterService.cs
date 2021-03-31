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
        IDataResult<List<CorrectorMaster>> GetAll();
        IDataResult<CorrectorMaster> GetById(int Id);
        IDataResult<List<CorrectorMaster>> GetByDeviceType(int deviceType);
        IDataResult<List<CorrectorMaster>> GetByCompany(int companyId);
        IResult Update(CorrectorMaster correctorMaster);
        IResult Add(CorrectorMaster correctorMaster);
    }
}
