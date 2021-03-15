using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            //var runtimeMethods = type.GetRuntimeMethods();
            //foreach (var runtimeMethod in runtimeMethods)
            //{
            //    var runtimeMethodAttributes= runtimeMethod.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            //    classAttributes.AddRange(runtimeMethodAttributes);
            //}


            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);


            return classAttributes.OrderBy(a => a.Priority).ToArray();
        }

        private static Type[] MethodParameterTypes(MethodInfo method)
        {
            var parameters = method.GetParameters();
            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < types.Length; i++)
            {
                if (!parameters[i].ParameterType.IsInterface)
                {
                    types[i] = parameters[i].ParameterType;
                }
            }

            return types.Where(el => el is not null).ToArray();
        }
    }
}
