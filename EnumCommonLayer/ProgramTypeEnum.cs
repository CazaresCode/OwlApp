using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumCommonLayer
{
    public class ProgramTypeEnum
    {
        public enum ProgramType
        {
            None,
            Vocal,
            Chamber,
            Conducting,
            [Display(Name ="Music History")]
            MusicHistory,
            Composition
        }
    }
}
