using Autofac;
using Core.DependencyResolvers.Autofac;
using FieldBusiness.DependencyResolvers.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public static class AutofacBusinessContainerBuilder
    {
        public static IContainer AutofacBusinessContainer { get; private set; }
        public static void CreateAutofacBusinessModuleContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            builder.RegisterModule<AutofacCoreModule>();
            builder.RegisterModule<AutofacFieldBusinessModule>();
            AutofacBusinessContainer = builder.Build();
        }
    }
}
