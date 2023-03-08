using System;



namespace ProxyApp
{
    public interface IBusinessModule
    {
        void Method();
    }

    public class BusinessModule : IBusinessModule
    {
        public void Method()
        {
            Console.WriteLine("Method.");
        }
    }

    public class BusinessModuleProxy : IBusinessModule
    {
        private BusinessModule _realObject;
        public BusinessModuleProxy()
        {
            _realObject = new BusinessModule();
        }
        public void Method()
        {
            Console.WriteLine("before method.");
            _realObject.Method();
            Console.WriteLine("after method.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var method = Activator.CreateInstance<IBusinessModule>();
            method.Method();
            Console.ReadKey();
        }
    }
}

