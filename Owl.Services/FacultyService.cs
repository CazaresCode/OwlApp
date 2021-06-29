using Owl.Data;
using Owl.Data.EntityModels;
using Owl.Models.FacultyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Services
{
    public class FacultyService
    {
        private readonly Guid _userId;

        public FacultyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFaculty(FacultyCreate model)
        {
            var entity =
                new Faculty()
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
                    IsPerforming = model.IsPerforming,
                    IsStaff = model.IsStaff
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Faculties.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FacultyListItem> GetFaculty()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Faculties
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new FacultyListItem
                                {
                                    Id = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    IsStaff = e.IsStaff,
                                    StartTime = e.StartTime,
                                    EndTime = e.EndTime
                                });

                return query.ToArray();
            }
        }

        public FacultyDetail GetFacultyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Faculties
                        .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    new FacultyDetail
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email,
                        PhoneNumber = entity.PhoneNumber,
                        TypeOfInstrument = entity.TypeOfInstrument,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                        HasFoodAllergy = entity.HasFoodAllergy,
                        FoodAllergy = entity.FoodAllergy,
                        IsPerforming = entity.IsPerforming,
                        IsStaff = entity.IsStaff
                    };
            }
        }

    }
}
