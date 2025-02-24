﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Data.Entities
{
    public class Vehicle
    {

        public int Id { get; set; }

        [Display(Name = "Placa")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Placa { get; set; }

        [Display(Name = "Modelo")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Modelo { get; set; }


        [Display(Name = "Color")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Color { get; set; }


        [Display(Name = "Price")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        public VehicleType VehicleType { get; set; }


        public Employee Employee { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<VehiclePhoto> VehiclePhotos { get; set; }

    }
}
