using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnumCommonLayer.InstrumentTypeEnum;

namespace Owl.Data.EntityModels
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

        [Required]
        [Display(Name = "Type of Instrument")]
        public InstrumentType TypeOfInstrument { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Has Food Allergy")]
        public bool HasFoodAllergy { get; set; }

        [Required]
        [Display(Name = "Food Allergy List")]
        public string FoodAllergy { get; set; }

    }
}
