using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnumCommonLayer.ProgramTypeEnum;

namespace Owl.Data.EntityModels
{
    public class Student : Person
    {
        [Required]
        [Display(Name = "Program Enrolled")]
        public ProgramType TypeOfProgram { get; set; }

        [Required]
        [Display(Name = "Has Paid Tuition")]
        public bool HasPaidTuition { get; set; }

        public virtual ICollection<Participation> Participations { get; set; }
    }
}
