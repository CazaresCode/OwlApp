using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Services
{
    public class ParticipationService
    {
        private readonly Guid _userId;

        public ParticipationService(Guid userId)
        {
            _userId = userId;
        }

        //public bool CreateParticipation (ParticipationCreate model)
        //{
        //    var entity =
        //        new Participation()
        //        {

        //        }
        //}
    }
}
