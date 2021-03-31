using Autofac;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHourlyArchiveParameterService
    {
        Task GetHourArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters);
        void AddArchiveParameterTransactionOperation(List<FieldHourlyArchiveParameter> hourlyArchiveParameter);
    }
}
