using System;

namespace DI
{
    public class A : iA
    {
        public A(iB b) { }
        public void showA()
        {
            Console.WriteLine("A");
        }
    }
}
