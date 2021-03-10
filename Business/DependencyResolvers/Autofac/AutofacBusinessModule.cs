using Autofac;
using Business.Abstract;
using Business.Concrete;
using FieldBusiness.Abstract;
using FieldBusiness.Concrete;
using FieldDataAccess.Abstract;
using FieldDataAccess.Concrete.Modbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HourlyArchiveParameterManager>().As<IHourlyArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<FieldHourArchiveParameterManager>().As<IFieldHourArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldHourlyArchiveParameterDal>().As<IFieldHourlyArchiveParameterDal>().InstancePerDependency();
        }
    }
}
