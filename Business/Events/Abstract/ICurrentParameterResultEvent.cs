﻿using Business.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Events.Abstract
{
    public interface ICurrentParameterResultEvent<TParameter>
    {
        event EventHandler<TParameter> OnCurrentDataIsReadyEvent;
    }
}
