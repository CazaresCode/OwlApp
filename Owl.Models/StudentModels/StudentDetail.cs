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
    public class StudentDetail
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Type of Instrument")]
        public InstrumentType TypeOfInstrument { get; set; }

        [Display(Name = "First Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Last Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Has Food Allergy")]
        public bool HasFoodAllergy { get; set; }

        [Display(Name = "Food Allergies")]
        public string FoodAllergy { get; set; }

        [Display(Name = "Program Enrolled In")]
        public ProgramType TypeOfProgram { get; set; }

        [Display(Name = "Has Paid Tuition")]
        public bool HasPaidTuition { get; set; }
    }
}
