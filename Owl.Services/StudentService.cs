﻿using Owl.Data;
using Owl.Data.EntityModels;
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
                                    FullName = e.FullName,
                                    TypeOfProgram = e.TypeOfProgram,
                                    StartTime = e.StartTime,
                                    EndTime = e.EndTime
                                });

                return query.ToArray();
            }
        }

    }
}
