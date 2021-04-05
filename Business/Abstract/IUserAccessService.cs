﻿using Core.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserAccessService
    {
        IDataResult<List<UserAccess>> GetByUserId(string userId);
        IResult Delete(UserAccess userAccess);
        IResult Add(UserAccess userAccess);
    }
}
