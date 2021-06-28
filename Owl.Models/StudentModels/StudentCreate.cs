using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnumCommonLayer.InstrumentTypeEnum;
using static EnumCommonLayer.ProgramTypeEnum;

namespace Owl.Models.StudentModels
{
    public class StudentCreate
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Type of Instrument")]
        public InstrumentType TypeOfInstrument { get; set; }

        [Required]
        [Display(Name = "First Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Last Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Has Food Allergy")]
        public bool HasFoodAllergy { get; set; }

        [Required]
        [Display(Name = "Food Allergy")]
        public string FoodAllergy { get; set; }

        [Required]
        [Display(Name = "Program Enrolled In")]
        public ProgramType TypeOfProgram { get; set; }

        [Required]
        [Display(Name = "Has Paid Tuition")]
        public bool HasPaidTuition { get; set; }
    }
}
