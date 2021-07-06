using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnumCommonLayer.ProgramTypeEnum;

namespace Owl.Models.StudentModels
{
    public class StudentListItem
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Program Enrolled In")]
        public ProgramType TypeOfProgram { get; set; }

        [Display(Name = "First Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Last Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Has Paid Tuition")]
        public bool HasPaidTuition { get; set; }

        //    public string HasPaidYesNo
        //    {
        //        get
        //        {
        //            if (HasPaidTuition)
        //            {
        //                return "Yes";
        //            }
        //            else
        //            {
        //                return "No";
        //            }
        //        }
        //    }
    }
}
