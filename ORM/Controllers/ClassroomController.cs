using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("School")]
    public class ClassroomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClassroomController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public List<Classroom> GetClassrooms() => _context.Classrooms.ToList();

        [HttpGet("{id}")]
        public ActionResult<Classroom> GetClassroom(int id)
        {
            var c = _context.Classrooms.Find(id);
            if (c == null) return NotFound();
            return c;
        }

        [HttpPost]
        public List<Classroom> PostClassroom([FromBody] Classroom classroom)
        {
            _context.Classrooms.Add(classroom);
            _context.SaveChanges();
            return _context.Classrooms.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Classroom>> PutClassroom(int id, [FromBody] Classroom updated)
        {
            var c = _context.Classrooms.Find(id);
            if (c == null) return NotFound();

            c.Number = updated.Number;
            c.Floor = updated.Floor;
            c.SeatCount = updated.SeatCount;
            c.Wing = updated.Wing;

            _context.SaveChanges();
            return Ok(_context.Classrooms);
        }

        [HttpDelete("{id}")]
        public List<Classroom> DeleteClassroom(int id)
        {
            var c = _context.Classrooms.Find(id);
            if (c == null) return _context.Classrooms.ToList();

            _context.Classrooms.Remove(c);
            _context.SaveChanges();
            return _context.Classrooms.ToList();
        }
    }
}