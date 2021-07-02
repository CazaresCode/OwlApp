using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Data.EntityModels
{
    public class Participation
    {
        [Key]
        public int Id { get; set; }

        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }


        [ForeignKey(nameof(Meeting))]
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
