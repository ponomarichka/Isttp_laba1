using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laba1_ISTTP
{
    public partial class City
    {
        public City()
        {
            Competition = new HashSet<Competition>();
            DanceStudio = new HashSet<DanceStudio>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "The field must contain only letters!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "Information")]
        public string Information { get; set; }

        public virtual ICollection<Competition> Competition { get; set; }
        public virtual ICollection<DanceStudio> DanceStudio { get; set; }
    }
}
