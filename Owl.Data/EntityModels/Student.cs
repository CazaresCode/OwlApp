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
        public ProgramType TypeOfProgram { get; set; }

        [Required]
        public bool HasPaidTuition { get; set; }
    }
}
