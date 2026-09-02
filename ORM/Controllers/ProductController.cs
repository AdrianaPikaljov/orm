using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace ORM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("Shop")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public List<Product> PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProduct(int id, [FromBody] Product updated)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Image = updated.Image;
            product.Active = updated.Active;
            product.Stock = updated.Stock;
            product.CategoryId = updated.CategoryId;

            _context.SaveChanges();
            return Ok(_context.Products);
        }

        [HttpDelete("{id}")]
        public List<Product> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return _context.Products.ToList();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }
    }
}