using System;

namespace DI
{
    public class B : iB
    {
        public B(iA a)
        {
        }

        public void showB()
        {
            Console.WriteLine("B");
        }
    }
}
