﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Data.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }
        [Display(Name = "Vehicle Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
