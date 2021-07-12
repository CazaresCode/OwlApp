using Owl.Data;
using Owl.Data.EntityModels;
using Owl.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owl.Services
{
    public class PersonService
    {
        private readonly Guid _userId;

        public PersonService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<PersonListItem> GetPeopleList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Persons
                        //Add this line...
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new PersonListItem
                                {
                                    Id = e.Id,
                                    FullName = e.FullName,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName, 
                                    StartTime = e.StartTime,
                                    EndTime = e.EndTime
                                });

                return query.ToArray();
            }
        }

        public PersonListItem GetPersonById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Persons
                        .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    new PersonListItem
                    {
                        Id = entity.Id,
                        FullName = entity.FullName,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime
                    };
            }
        }
    }
}
