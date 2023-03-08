namespace CastleDynamicProxyFirstApp
{
    public interface IMyClass
    {
        void MyMethod();
    }
    [MyInterceptorAspect(Priority = 1)]
    public class MyClass : IMyClass
    {
        public virtual void MyMethod()
        {
            Console.WriteLine("My Method Body");
        }
    }
}

