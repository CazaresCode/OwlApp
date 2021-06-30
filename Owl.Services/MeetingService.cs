using Owl.Data;
using Owl.Data.EntityModels;
using Owl.Models.MeetingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Services
{
    public class MeetingService
    {
        private readonly Guid _userId;

        public MeetingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMeeting(MeetingCreate model)
        {
            var entity =
                new Meeting()
                {
                    OwnerId = _userId,
                    NameOfMeeting = model.NameOfMeeting,
                    Description = model.Description,
                    Location = model.Location,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    TypeOfMeeting = model.TypeOfMeeting
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Meetings.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MeetingListItem> GetMeetings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Meetings
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new MeetingListItem
                                {
                                    Id = e.Id,
                                    NameOfMeeting = e.NameOfMeeting,
                                    TypeOfMeeting = e.TypeOfMeeting
                                });

                return query.ToArray();
            }
        }

        public MeetingDetail GetMeetingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Meetings
                        .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    new MeetingDetail
                    {
                        Id = entity.Id,
                        NameOfMeeting = entity.NameOfMeeting,
                        Description = entity.Description,
                        Location = entity.Location,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                        TypeOfMeeting = entity.TypeOfMeeting
                    };
            }
        }



        //EDIT
    }
}
