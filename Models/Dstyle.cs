using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Laba1_ISTTP
{
    public partial class Dstyle
    {
        public Dstyle()
        {
            Choreography = new HashSet<Choreography>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "The field must contain only letters!")]
        [Display(Name= "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
       
        [Display(Name = "Information")]
        public string Information { get; set; }

        public virtual ICollection<Choreography> Choreography { get; set; }
    }
}
