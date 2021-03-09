using Autofac;
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
            AutofacBusinessContainer = builder.Build();
        }
    }
}
