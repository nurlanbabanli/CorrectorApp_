﻿using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldDataAccess.Abstract
{
    public interface IFieldCurrentParameterDal
    {
        Task<List<FieldCurrentParameter>> GetFieldArchiveParametersAsync(CorrectorMaster deviceParameter);
    }
}
