
using Autofac;
using Castle.DynamicProxy;
using Entities;
using System;



namespace InvocationApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultModule());
            var container = builder.Build();
            var willbeintercepted = container.Resolve<IEmployee>();
            var emp1 = new Employee()
            {
                Id = 1,
                FirstName = "can",
                LastName = "kutay"
            };

            willbeintercepted.Add(emp1.Id, emp1.FirstName, emp1.LastName);
            Console.ReadKey();
        }
    }
}

