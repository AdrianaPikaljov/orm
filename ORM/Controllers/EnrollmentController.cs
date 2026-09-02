using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("School")]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public List<Enrollment> GetEnrollments() => _context.Enrollments.ToList();

        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollment(int id)
        {
            var e = _context.Enrollments.Find(id);
            if (e == null) return NotFound();
            return e;
        }

        [HttpPost]
        public List<Enrollment> PostEnrollment([FromBody] Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
            return _context.Enrollments.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Enrollment>> PutEnrollment(int id, [FromBody] Enrollment updated)
        {
            var e = _context.Enrollments.Find(id);
            if (e == null) return NotFound();

            e.Grade = updated.Grade;

            _context.SaveChanges();
            return Ok(_context.Enrollments);
        }

        [HttpDelete("{id}")]
        public List<Enrollment> DeleteEnrollment(int id)
        {
            var e = _context.Enrollments.Find(id);
            if (e == null) return _context.Enrollments.ToList();

            _context.Enrollments.Remove(e);
            _context.SaveChanges();
            return _context.Enrollments.ToList();
        }

        // kõik ühe õpilase ained
        [HttpGet("student/{studentId}")]
        public List<Enrollment> GetEnrollmentsForStudent(int studentId) =>
            _context.Enrollments.Where(e => e.StudentId == studentId).ToList();

        // kõik ühe aine õpilased
        [HttpGet("subject/{subjectId}")]
        public List<Enrollment> GetEnrollmentsForSubject(int subjectId) =>
            _context.Enrollments.Where(e => e.SubjectId == subjectId).ToList();
    }
}