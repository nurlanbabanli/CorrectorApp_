using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Interceptors;
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


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //var assemblyies = AppDomain.CurrentDomain.GetAssemblies()
            //    .Where(asm => asm.FullName == "Business, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"| 
            //    asm.FullName == "FieldBusiness, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").ToList();


            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).InstancePerDependency();

        }
    }
}
