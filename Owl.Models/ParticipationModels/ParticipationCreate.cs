using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Models.ParticipationModels
{
    public class ParticipationCreate
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        public int MeetingId { get; set; }
    }
}
