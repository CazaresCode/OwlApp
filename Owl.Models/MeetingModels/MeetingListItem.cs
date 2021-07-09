using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnumCommonLayer.MeetingTypeEnum;

namespace Owl.Models.MeetingModels
{
    public class MeetingListItem
    {
        public int Id { get; set; }

        [Display(Name = "Name of Meeting")]
        public string NameOfMeeting { get; set; }

        [Display(Name = "Meeting Type")]
        public MeetingType TypeOfMeeting { get; set; }

        [Display(Name = "First Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Last Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
    }
}
