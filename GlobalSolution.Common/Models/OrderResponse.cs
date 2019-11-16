using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalSolution.Common.Models
{
   public class OrderResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Garantia { get; set; }
        public string Trabajo { get; set; }
        public DateTime EntryDate1 { get; set; }
        public DateTime EntryDate1Local => EntryDate1Local.ToLocalTime();
        public CustomerResponse Customer { get; set; }
        public JobTypeResponse JobType { get; set; }
    }
}
