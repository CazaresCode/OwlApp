using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Data.EntityModels
{
    public class Participation
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }
        public virtual Person Persons { get; set; }

        [Required]
        public int MeetingId { get; set; }
        public virtual Meeting Meetings { get; set; }
    }
}
