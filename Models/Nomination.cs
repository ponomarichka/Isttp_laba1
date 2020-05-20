using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laba1_ISTTP
{
    public partial class Nomination
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "Nomination")]
        public int NomListId { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [Display(Name = "Place")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "Dancer")]
        public int DancerId { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "Choreography")]
        public int ChoreographyId { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "Competition")]
        public int CompetitionId { get; set; }

        public virtual Choreography Choreography { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual Dancer Dancer { get; set; }
        public virtual NominationList NomList { get; set; }
    }
}
