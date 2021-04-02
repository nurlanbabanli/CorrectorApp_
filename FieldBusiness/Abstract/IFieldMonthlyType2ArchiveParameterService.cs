﻿using Core.ActionReports;
using Core.Events.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Abstract
{
    public interface IFieldMonthlyType2ArchiveParameterService : IResultEvent<FieldMonthlyType2ArchiveParameter, IProgress<ProgressStatus>>
    {
        Task GetMonthlyType2ArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter);
    }
}
