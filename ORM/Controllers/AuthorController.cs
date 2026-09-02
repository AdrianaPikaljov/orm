using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM.Data;
using ORM.Models;

namespace ORM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("News")]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public List<Author> PostAuthor([FromBody] Author author)
        {
            _context.ContactDatas.Add(author.Contact);
            _context.SaveChanges();

            author.ContactDataId = author.Contact.Id;

            _context.Authors.Add(author);
            _context.SaveChanges();
            return _context.Authors.Include(a => a.Contact).ToList();
        }

        [HttpGet]
        public List<Author> GetAuthors()
        {
            var authors = _context.Authors.Include(a => a.Contact).ToList();
            return authors;
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _context.Authors.Include(a => a.Contact).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpDelete("{id}")]
        public List<Author> DeleteAuthor(int id)
        {
            var author = _context.Authors.Include(a => a.Contact).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return _context.Authors.Include(a => a.Contact).ToList();
            }

            if (author.Contact != null)
            {
                _context.ContactDatas.Remove(author.Contact);
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return _context.Authors.Include(a => a.Contact).ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Author>> PutAuthor(int id, [FromBody] Author updatedAuthor)
        {
            var author = _context.Authors.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            author.FirstName = updatedAuthor.FirstName;
            author.LastName = updatedAuthor.LastName;
            author.PersonalCode = updatedAuthor.PersonalCode;

            var contactData = _context.ContactDatas.Find(author.ContactDataId);
            if (contactData != null)
            {
                contactData.Address = updatedAuthor.Contact.Address;
                contactData.Phone = updatedAuthor.Contact.Phone;
            }

            _context.SaveChanges();

            return Ok(_context.Authors);
        }
    }
}