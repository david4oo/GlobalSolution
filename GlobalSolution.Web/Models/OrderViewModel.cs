using GlobalSolution.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Models
{
    public class OrderViewModel : Order
    {


        public int EmployeeId { get; set; }

        public int VehicleId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Customer.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "JobType")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a JobType.")]
        public int JobTypeId { get; set; }



        public IEnumerable<SelectListItem> Customers { get; set; }

        public IEnumerable<SelectListItem> JobTypes { get; set; }




    }
}

