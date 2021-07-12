using Owl.Data;
using Owl.Data.EntityModels;
using Owl.Models.MeetingModels;
using Owl.Models.ParticipationModels;
using Owl.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Services
{
    public class StudentService
    {
        private readonly Guid _userId;

        public StudentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateStudent(StudentCreate model)
        {
            var entity =
                new Student()
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    TypeOfInstrument = model.TypeOfInstrument,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    HasFoodAllergy = model.HasFoodAllergy,
                    FoodAllergy = model.FoodAllergy,
                    TypeOfProgram = model.TypeOfProgram,
                    HasPaidTuition = model.HasPaidTuition
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Students.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<StudentListItem> GetStudents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Students
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new StudentListItem
                                {
                                    Id = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    TypeOfProgram = e.TypeOfProgram,
                                    StartTime = e.StartTime,
                                    EndTime = e.EndTime,
                                    HasPaidTuition = e.HasPaidTuition,
                                    HasFoodAllergy = e.HasFoodAllergy
                                });

                return query.ToArray();
            }
        }

        public StudentDetail GetStudentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Students
                        .SingleOrDefault(e => e.Id == id && e.OwnerId == _userId);
                return
                    new StudentDetail
                    {
                        Id = entity.Id,
                        FullName = entity.FullName,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email,
                        PhoneNumber = entity.PhoneNumber,
                        TypeOfInstrument = entity.TypeOfInstrument,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                        HasFoodAllergy = entity.HasFoodAllergy,
                        FoodAllergy = entity.FoodAllergy,
                        TypeOfProgram = entity.TypeOfProgram,
                        HasPaidTuition = entity.HasPaidTuition,
                       // Meetings that are tied to this studentS
                        Meetings = (List<MeetingListItem>)entity.Participations
                                        .Select(m=>
                                        new MeetingListItem
                                        {
                                            Id = m.Id,
                                            NameOfMeeting = m.Meeting.NameOfMeeting,
                                            TypeOfMeeting = m.Meeting.TypeOfMeeting,
                                            StartTime = m.Meeting.StartTime,
                                            EndTime = m.Meeting.EndTime
                                        })
                    };
            }
        }

        public bool UpdateStudent(StudentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Students
                        .Single(e => e.Id == model.Id && e.OwnerId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;
                entity.PhoneNumber = model.PhoneNumber;
                entity.TypeOfInstrument = model.TypeOfInstrument;
                entity.StartTime = model.StartTime;
                entity.EndTime = model.EndTime;
                entity.HasFoodAllergy = model.HasFoodAllergy;
                entity.FoodAllergy = model.FoodAllergy;
                entity.TypeOfProgram = model.TypeOfProgram;
                entity.HasFoodAllergy = model.HasFoodAllergy;
                entity.HasPaidTuition = model.HasPaidTuition;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteStudent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Students
                        .Single(e => e.Id == id && e.OwnerId == _userId);

                ctx.Students.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
