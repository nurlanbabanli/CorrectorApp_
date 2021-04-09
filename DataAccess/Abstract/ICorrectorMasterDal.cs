using Core.DataAccess.Mssql.EntityFramework;
using Core.Results.Abstract;
using DataAccess.Concrete.Database.Mssql.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICorrectorMasterDal:IEfEntityRepository<CorrectorMaster>
    {
    }
}
