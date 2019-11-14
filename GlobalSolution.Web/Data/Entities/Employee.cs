using System.Collections.Generic;

namespace GlobalSolution.Web.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<Order> Orders { get; set; }


    }

}

