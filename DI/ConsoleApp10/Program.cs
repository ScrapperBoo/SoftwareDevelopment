namespace DI
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DI();
            container.AddUnstable<iA, A>();
            container.AddUnstable<iB, B>();
            container.Get<iB>();
        }
    }
}
