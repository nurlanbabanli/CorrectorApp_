using Core.DataAccess.Mssql.EntityFramework;
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
    public class MssqlEfCorrectorMasterDal :EfEntityRepositoryBase<CorrectorMaster,MssqlCorrectorContext>,  ICorrectorMasterDal
    {
       
    }
}
