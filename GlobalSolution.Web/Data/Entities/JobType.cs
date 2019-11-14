using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Data.Entities
{
    public class JobType
    {
        public int Id { get; set; }
        [Display(Name = "Job Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
