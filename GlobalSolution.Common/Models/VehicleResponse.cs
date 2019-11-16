using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalSolution.Common.Models
{
   public class VehicleResponse
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string VehicleType { get; set; }

        public ICollection<VehiclePhotoResponse> VehiclePhotos { get; set; }

        public ICollection<OrderResponse> Orders { get; set; }


    }
}
