using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace ORM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("Shop")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return category;
        }

        [HttpPost]
        public List<Category> PostCategory([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutCategory(int id, [FromBody] Category updated)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            category.Name = updated.Name;

            _context.SaveChanges();
            return Ok(_context.Categories);
        }

        [HttpDelete("{id}")]
        public List<Category> DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return _context.Categories.ToList();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }
    }
}