using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("School")]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public List<Teacher> GetTeachers() => _context.Teachers.ToList();

        [HttpGet("{id}")]
        public ActionResult<Teacher> GetTeacher(int id)
        {
            var t = _context.Teachers.Find(id);
            if (t == null) return NotFound();
            return t;
        }

        [HttpPost]
        public List<Teacher> PostTeacher([FromBody] Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return _context.Teachers.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Teacher>> PutTeacher(int id, [FromBody] Teacher updated)
        {
            var t = _context.Teachers.Find(id);
            if (t == null) return NotFound();

            t.FirstName = updated.FirstName;
            t.LastName = updated.LastName;
            t.PersonalCode = updated.PersonalCode;
            t.Email = updated.Email;
            t.Degree = updated.Degree;

            _context.SaveChanges();
            return Ok(_context.Teachers);
        }

        [HttpDelete("{id}")]
        public List<Teacher> DeleteTeacher(int id)
        {
            var t = _context.Teachers.Find(id);
            if (t == null) return _context.Teachers.ToList();

            _context.Teachers.Remove(t);
            _context.SaveChanges();
            return _context.Teachers.ToList();
        }
    }
}