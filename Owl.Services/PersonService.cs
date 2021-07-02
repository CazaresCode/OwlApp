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
        public IEnumerable<PersonListItem> GetPeopleList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Persons
                        .Select(
                            e =>
                                new PersonListItem
                                {
                                    Id = e.Id,
                                    FullName = e.FullName
                                });

                return query.ToArray();
            }
        }

        public IEnumerable<Person> GetPeople()
        {
            using(var ctx = new ApplicationDbContext())
            {
                return ctx.Persons.ToList();
            }
        }
    }
}
