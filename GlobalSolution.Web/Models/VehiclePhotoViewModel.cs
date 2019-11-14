using GlobalSolution.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Models
{
    public class VehiclePhotoViewModel : VehiclePhoto
    {

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


    }
}
