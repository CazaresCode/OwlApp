using Owl.Models.MeetingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnumCommonLayer.InstrumentTypeEnum;
using static EnumCommonLayer.MeetingTypeEnum;

namespace Owl.Models.FacultyModels
{
    public class FacultyDetail
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Last Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Has Food Allergy")]
        public bool HasFoodAllergy { get; set; }

        [Display(Name = "Food Allergies")]
        public string FoodAllergy { get; set; }

        [Display(Name = "Is Staff")]
        public bool IsStaff { get; set; }

        [Display(Name = "Is Performing")]
        public bool IsPerforming { get; set; }

        public List<MeetingListItem> Meetings { get; set; }

        public int MeetingId { get; set; }

        [Display(Name = "Name of Meeting")]
        public string NameOfMeeting { get; set; }

        [Display(Name = "Meeting Type")]
        public MeetingType TypeOfMeeting { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime MeetingStartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime MeetingEndTime { get; set; }
    }
}
