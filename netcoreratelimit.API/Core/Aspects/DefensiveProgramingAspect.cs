using Castle.DynamicProxy;
using Core.Interceptors;

namespace Core.Aspects
{
    public class DefensiveProgramingAspect : MethodInterception
    {
        public override void Onbefore(IInvocation invocation)
        {
            var parameters = invocation.Arguments;
            foreach (var parameter in parameters)
            {
                if (parameter.Equals(null))
                {
                    throw new ArgumentNullException();
                }
            }
            Console.WriteLine("Null check has been completed for {0}", invocation.Method);
        }
    }
}
