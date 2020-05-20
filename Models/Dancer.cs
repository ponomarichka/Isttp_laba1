using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laba1_ISTTP
{
    public partial class Dancer
    {
        public Dancer()
        {
            Choreography = new HashSet<Choreography>();
            Nomination = new HashSet<Nomination>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "The field must contain only letters!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthday")]
        public DateTime  Birthday { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]

        [Display(Name = "Information")]
        public string Information { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [Display(Name = "Dance Studio")]
        public int DanceStudioId { get; set; }

        public virtual DanceStudio DanceStudio { get; set; }
        public virtual ICollection<Choreography> Choreography { get; set; }
        public virtual ICollection<Nomination> Nomination { get; set; }
    }
}
