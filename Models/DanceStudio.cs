using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laba1_ISTTP
{
    public partial class DanceStudio
    {
        public DanceStudio()
        {
            Dancer = new HashSet<Dancer>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "The field must contain only letters!")]
        [Display(Name = "Name ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Сan’t be empty")]
        [Display(Name = "Adress")]
        public string Adress { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Dancer> Dancer { get; set; }
    }
}
