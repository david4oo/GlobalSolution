using GlobalSolution.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Models
{
    public class VehicleViewModel : Vehicle
    {

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Vehicle type.")]
        public int VehicleTypeId { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

    }


}
