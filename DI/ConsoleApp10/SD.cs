using System;

namespace DI
{
    public class SD
    {
        public Type ServiceType { get; }

        public Type ImplementationType { get; }

        public object Implementation { get; internal set; }

        public ST LifeTime { get; }

        public SD(Type sT, Type IT, ST lT)
        {
            ServiceType  = sT;
            ImplementationType = IT;
            LifeTime = lT;
        }
    }
}
