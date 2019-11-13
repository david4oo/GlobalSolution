﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

