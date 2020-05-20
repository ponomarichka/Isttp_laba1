using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laba1_ISTTP
{
    public partial class NominationList
    {
        public NominationList()
        {
            Nomination = new HashSet<Nomination>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "The field must contain only letters!")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Nomination> Nomination { get; set; }
    }
}
