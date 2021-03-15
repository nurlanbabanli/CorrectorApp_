﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results.Abstract
{
    public interface IResult
    {
        string Message { get; }
        List<string> MessageList { get; }
        bool IsSuccess { get; }
    }
}
