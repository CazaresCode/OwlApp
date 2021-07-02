using Owl.Models.MeetingModels;
using Owl.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Models.ParticipationModels
{
    public class ParticipationDetail
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public PersonListItem Person { get; set; }
        public int MeetingId { get; set; }
        public MeetingListItem Meeting { get; set; }
    }
}
