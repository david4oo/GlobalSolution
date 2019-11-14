using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolution.Web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "warranty")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public string Garantia { get; set; }

        [Display(Name = "Work Duration")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public string Trabajo { get; set; }

        [Display(Name = " Entry Vehicle")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate1 { get; set; }

        [Display(Name = " Entry Vehicle")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate1Local => EntryDate1Local.ToLocalTime();

        public Vehicle Vehicle { get; set; }

        public Employee Employee { get; set; }

        public Customer Customer { get; set; }

        public JobType JobType { get; set; }

    }
}
