using Castle.DynamicProxy;
using Core.Interceptors;

namespace CastleDynamicProxyFirstApp
{
    public class MyInterceptorAspect : MethodInterception
    {
        public override void Onbefore(IInvocation invocation)
        {
            Console.WriteLine($"Before: {invocation.Method}");
        }
        public override void OnAfter(IInvocation invocation)
        {
            Console.WriteLine($"After: {invocation.Method}");
        }
    }
}

