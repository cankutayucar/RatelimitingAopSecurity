using Core.Aspects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IEmployee
    {
        void Add(int id, string firstName, string lastName);
        void Update(int id, string firstName, string lastName);
    }
    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DefensiveProgramingAspect(Priority = 1)]
        [InterceptionAspect(Priority = 2)]
        public virtual void Add(int id, string firstName, string lastName)
        {
            Console.WriteLine("Added");
        }


        [DefensiveProgramingAspect(Priority = 2)]
        [InterceptionAspect(Priority = 1)]
        public virtual void Update(int id, string firstName, string lastName)
        {
            Console.WriteLine("Updated");
        }
    }
}
