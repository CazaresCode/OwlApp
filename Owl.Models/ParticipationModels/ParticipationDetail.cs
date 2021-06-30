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
        public int MeetingId { get; set; }
    }
}
