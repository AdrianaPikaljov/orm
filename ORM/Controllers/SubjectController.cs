using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM.Data;
using ORM.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("School")]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SubjectController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public List<Subject> GetSubjects() =>
            _context.Subjects.Include(s => s.Teacher).Include(s => s.Classroom).ToList();

        [HttpGet("{id}")]
        public ActionResult<Subject> GetSubject(int id)
        {
            var subject = _context.Subjects
                .Include(s => s.Teacher)
                .Include(s => s.Classroom)
                .FirstOrDefault(s => s.Id == id);

            if (subject == null) return NotFound();
            return subject;
        }

        [HttpPost]
        public List<Subject> PostSubject([FromBody] Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return _context.Subjects.Include(s => s.Teacher).Include(s => s.Classroom).ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Subject>> PutSubject(int id, [FromBody] Subject updated)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();

            subject.Name = updated.Name;
            subject.Credits = updated.Credits;
            subject.TeacherId = updated.TeacherId;
            subject.ClassroomId = updated.ClassroomId;

            _context.SaveChanges();
            return Ok(_context.Subjects);
        }

        [HttpDelete("{id}")]
        public List<Subject> DeleteSubject(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return _context.Subjects.ToList();

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return _context.Subjects.ToList();
        }
    }
}