using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM.Data;
using ORM.Models;

namespace ORM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Tags("Shop")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            return _context.Orders.Include(o => o.CartProduct).Include(o => o.Person).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders.Include(o => o.CartProduct).Include(o => o.Person).FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return order;
        }

        [HttpPost]
        public List<Order> PostOrder([FromBody] Order order)
        {
            order.Created = DateTime.Now;
            _context.Orders.Add(order);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updated)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            order.TotalSum = updated.TotalSum;
            order.Paid = updated.Paid;

            _context.SaveChanges();
            return Ok(_context.Orders);
        }

        [HttpDelete("{id}")]
        public List<Order> DeleteOrder(int id)
        {
            var order = _context.Orders.Include(o => o.CartProduct).FirstOrDefault(o => o.Id == id);
            if (order == null) return _context.Orders.ToList();

            if (order.CartProduct != null)
            {
                _context.CartProducts.RemoveRange(order.CartProduct);
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }
    }
}