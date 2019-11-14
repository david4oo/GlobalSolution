using System.Collections.Generic;

namespace GlobalSolution.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
