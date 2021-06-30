﻿using Owl.Data;
using Owl.Data.EntityModels;
using Owl.Models.ParticipationModels;
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

        public bool CreateParticipation(ParticipationCreate model)
        {
            var entity =
                new Participation()
                {
                    OwnerId = _userId,
                    PersonId = model.PersonId,
                    MeetingId = model.MeetingId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Participations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ParticipationListItem> GetParticipations()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Participations
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ParticipationListItem
                                {
                                    Id = e.Id,
                                    PersonId = e.PersonId,
                                    MeetingId = e.MeetingId
                                });

                return query.ToArray();
            }
        }

        public ParticipationDetail GetParticipationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Participations
                        .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    new ParticipationDetail
                    {
                        Id = entity.Id,
                        PersonId = entity.PersonId,
                        
                        MeetingId = entity.MeetingId
                    };
            }
        }
    }
}
