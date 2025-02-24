﻿using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        // TODO: Change the path when publish

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
        ? null
        : $"https://TBD.azurewebsites.net{ImageUrl.Substring(1)}";


        public Vehicle Vehicle { get; set; }

    }
}
