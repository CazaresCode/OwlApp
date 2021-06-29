using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Data.EntityModels
{
    public class Faculty : Person
    {
        [Required]
        [Display(Name = "Is Staff")]
        public bool IsStaff { get; set; }

        [Required]
        [Display(Name = "Is Performing")]
        public bool IsPerforming { get; set; }

        //public virtual ICollection<Participation> Participations { get; set; }
    }
}
