using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.Database.Mssql.EntityFramework;
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
            builder.RegisterType<FieldHourlyArchiveParameterManager>().As<IFieldHourlyArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldHourlyArchiveParameterDal>().As<IFieldHourlyArchiveParameterDal>().InstancePerDependency();
            builder.RegisterType<MssqlEfHourlyArchiveParameterDal>().As<IHourlyArchiveParameterDal>().InstancePerDependency();

            builder.RegisterType<DailyArchiveParameterManager>().As<IDailyArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<FieldDailyArchiveParameterManager>().As<IFieldDailyArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldDailyArchiveParameterDal>().As<IFieldDailyArchiveParameterDal>().InstancePerDependency();
            builder.RegisterType<MssqlEfDailyArchiveParameterDal>().As<IDailyArchiveParameterDal>().InstancePerDependency();

            builder.RegisterType<EventArchiveParameterManager>().As<IEventArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<FieldEventArchiveParameterManager>().As<IFieldEventArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldEventArchiveParameterDal>().As<IFieldEventArchiveParameterDal>().InstancePerDependency();
            builder.RegisterType<MssqlEfEventArchiveParameterDal>().As<IEventArchiveParameterDal>().InstancePerDependency();

            builder.RegisterType<MonthlyType1ArchiveParameterManager>().As<IMonthlyType1ArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<FieldMonthlyType1ArchiveParameterManager>().As<IFieldMonthlyType1ArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldMonthlyType1ArchiveParameterDal>().As<IFieldMonthlyType1ArchiveParameterDal>().InstancePerDependency();
            builder.RegisterType<MssqlEfMonthlyType1ArchiveParameterDal>().As<IMonthlyType1ArchiveParameterDal>().InstancePerDependency();

            builder.RegisterType<MonthlyType2ArchiveParameterManager>().As<IMonthlyType2ArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<FieldMonthlyType2ArchiveParameterManager>().As<IFieldMonthlyType2ArchiveParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldMonthlyType2ArchiveParameterDal>().As<IFieldMonthlyType2ArchiveParameterDal>().InstancePerDependency();
            builder.RegisterType<MbFieldMonthlyType2ArchivePart1PArameterDal>().As<IFieldMonthlyType2ArchivePart1ParameterDal>().InstancePerDependency();
            builder.RegisterType<MbFieldMonthlyType2ArchivePart2PArameterDal>().As<IFieldMonthlyType2ArchivePart2ParameterDal>().InstancePerDependency();
            builder.RegisterType<MbFieldMonthlyType2ArchivePart3PArameterDal>().As<IFieldMonthlyType2ArchivePart3ParameterDal>().InstancePerDependency();
            builder.RegisterType<MssqlEfMonthlyType2ArchiveParameterDal>().As<IMonthlyType2ArchiveParameterDal>().InstancePerDependency();           

            builder.RegisterType<CurrentParameterManager>().As<ICurrentParameterService>().InstancePerDependency();
            builder.RegisterType<FieldCurrentParameterManager>().As<IFieldCurrentParameterService>().InstancePerDependency();
            builder.RegisterType<MbFieldCurrentParameterDal>().As<IFieldCurrentParameterDal>().InstancePerDependency();

            builder.RegisterType<CorrectorMasterManager>().As<ICorrectorMasterService>().InstancePerDependency();
            builder.RegisterType<MssqlEfCorrectorMasterDal>().As<ICorrectorMasterDal>().InstancePerDependency();

            builder.RegisterType<WinFormAuthManager>().As<IWinFormAuthService>().InstancePerDependency();
            builder.RegisterType<UserManager>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<MssqlEfUserDal>().As<IUserDal>().InstancePerDependency();
            builder.RegisterType<UserAccessManager>().As<IUserAccessService>().InstancePerDependency();
            builder.RegisterType<MssqlEfUserAccessDal>().As<IUserAccessDal>().InstancePerDependency();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();


            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).InstancePerDependency();
   
        }
    }
}
