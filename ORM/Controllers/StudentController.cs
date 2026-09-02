using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("School")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public List<Student> GetStudents() => _context.Students.ToList();

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var s = _context.Students.Find(id);
            if (s == null) return NotFound();
            return s;
        }

        [HttpPost]
        public List<Student> PostStudent([FromBody] Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return _context.Students.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Student>> PutStudent(int id, [FromBody] Student updated)
        {
            var s = _context.Students.Find(id);
            if (s == null) return NotFound();

            s.FirstName = updated.FirstName;
            s.LastName = updated.LastName;
            s.PersonalCode = updated.PersonalCode;
            s.Email = updated.Email;

            _context.SaveChanges();
            return Ok(_context.Students);
        }

        [HttpDelete("{id}")]
        public List<Student> DeleteStudent(int id)
        {
            var s = _context.Students.Find(id);
            if (s == null) return _context.Students.ToList();

            _context.Students.Remove(s);
            _context.SaveChanges();
            return _context.Students.ToList();
        }
    }
}