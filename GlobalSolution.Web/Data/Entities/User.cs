using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "{0} field can not have more that {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(20, ErrorMessage = "{0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = "{0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [MaxLength(50, ErrorMessage = "{0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Employee Name")]
        public string FullName => $"{FirstName} {LastName}";
        [Display(Name = "Employee Name")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }
}
