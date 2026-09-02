using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace ORM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("Shop")]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Person> GetPeople()
        {
            return _context.People.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _context.People.Find(id);
            if (person == null) return NotFound();
            return person;
        }

        [HttpPost]
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
            return _context.People.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updated)
        {
            var person = _context.People.Find(id);
            if (person == null) return NotFound();

            person.PersonCode = updated.PersonCode;
            person.FirstName = updated.FirstName;
            person.LastName = updated.LastName;
            person.Phone = updated.Phone;
            person.Address = updated.Address;
            person.Password = updated.Password;
            person.Admin = updated.Admin;

            _context.SaveChanges();
            return Ok(_context.People);
        }

        [HttpDelete("{id}")]
        public List<Person> DeletePerson(int id)
        {
            var person = _context.People.Find(id);
            if (person == null) return _context.People.ToList();

            _context.People.Remove(person);
            _context.SaveChanges();
            return _context.People.ToList();
        }
    }
}