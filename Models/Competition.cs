using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laba1_ISTTP
{
    public partial class Competition
    {
        public Competition() 
        {
            Nomination = new HashSet<Nomination>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "The field must contain only letters!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]

        [Display(Name = "Information")]
        public string Information { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
       
        [Display(Name = "Organizer")]
        public int OrganizerId { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "City")]
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Organizer Organizer { get; set; }
        public virtual ICollection<Nomination> Nomination { get; set; }
    }
}
