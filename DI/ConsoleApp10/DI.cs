using System;
using System.Collections.Generic;
using System.Linq;

namespace DI
{
    public class DI
    {
        private List<SD> dependencies;
        public DI()
        {
            dependencies = new List<SD>();
        }
        public void AddUnstable<TService, TImplementation>()
        {
            dependencies.Add(new SD(typeof(TService), typeof(TImplementation), ST.Transient));
        }
        public void AddSingleton<TService, TImplementation>()
        {
            dependencies.Add(new SD(typeof(TService), typeof(TImplementation), ST.Singleton));
        }
        public T Get<T>() => (T)Get(typeof(T));
        public object Get(Type sT)
        {
            var specifier = dependencies.SingleOrDefault(x => x.ServiceType == sT);
            if (specifier == null)
            {
                throw new Exception("not found");
            }
            if (specifier.Implementation != null)
            {
                return specifier.Implementation;
            }
            var relevantType = specifier.ImplementationType;
            var constInfo = relevantType.GetConstructors().First();
            if (constInfo.GetParameters().Any(x => chekCycle(sT, x.ParameterType)))
            {
                throw new Exception("Цикл");
            }
            var parameters = constInfo.GetParameters().Select(x => Get(x.ParameterType)).ToArray();
            var implementation = Activator.CreateInstance(relevantType, parameters);
            if (specifier.LifeTime == ST.Singleton)
            {
                specifier.Implementation = implementation;
            }
            return implementation;
        }
        public bool chekCycle(Type serviceType, Type parametrType)
        {
            var descriptor = dependencies.SingleOrDefault(x => x.ServiceType == parametrType);
            var actualType = descriptor.ImplementationType;
            var constructorType = actualType.GetConstructors().First();
            return constructorType.GetParameters().Any(x => Equals(serviceType, x.ParameterType));
        }
    }
}
