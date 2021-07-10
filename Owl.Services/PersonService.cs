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

        //public IEnumerable<Person> GetPeople()
        //{
        //    using(var ctx = new ApplicationDbContext())
        //    {
        //        return ctx.Persons.ToList();
        //    }
        //}
    }
}
