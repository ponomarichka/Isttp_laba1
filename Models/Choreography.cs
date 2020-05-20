using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Laba1_ISTTP
{
    public partial class Choreography
    {
        public Choreography()
        {
            Nomination = new HashSet<Nomination>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$",ErrorMessage = "The field must contain only letters!")]
        [Display(Name = "Music")]
        public string Music { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "Dancer")]
        public int DancerId { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [Display(Name = "Style")]
        public int StyleId { get; set; }

        public virtual Dancer Dancer { get; set; }
        public virtual Dstyle Style { get; set; }
        public virtual ICollection<Nomination> Nomination { get; set; }
    }
}
