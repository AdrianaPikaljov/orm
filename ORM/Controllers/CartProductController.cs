using Microsoft.AspNetCore.Mvc;
using ORM.Data;
using ORM.Models;

namespace ORM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("Shop")]
    public class CartProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CartProduct> GetCartProducts()
        {
            return _context.CartProducts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var cartProduct = _context.CartProducts.Find(id);
            if (cartProduct == null) return NotFound();
            return cartProduct;
        }

        [HttpPost]
        public List<CartProduct> PostCartProduct([FromBody] CartProduct cartProduct)
        {
            _context.CartProducts.Add(cartProduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        [HttpDelete("{id}")]
        public List<CartProduct> DeleteCartProduct(int id)
        {
            var cartProduct = _context.CartProducts.Find(id);
            if (cartProduct == null) return _context.CartProducts.ToList();

            _context.CartProducts.Remove(cartProduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<List<CartProduct>> GetCartProductsForOrder(int orderId)
        {
            var cartProducts = _context.CartProducts.Where(c => c.OrderId == orderId).ToList();
            if (cartProducts.Count == 0) return NotFound();
            return cartProducts;
        }
    }
}