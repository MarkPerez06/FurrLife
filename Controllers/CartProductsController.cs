using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FurrLife.Controllers
{
    public class CartProductsController : Controller
    {
        private readonly ILogger<CartProductsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public CartProductsController(ApplicationDbContext context, ILogger<CartProductsController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var model = from cp in _context.CartProducts
                        join p in _context.Products
                        on cp.ProductId equals p.Id
                        join m in _context.Menu
                        on p.MenuId equals m.Id
                        join u in _context.Units
                        on p.UnitId equals u.Id
                        where cp.SessionId == HttpContext.Session.GetString("SessionId")
                        orderby cp.Id descending
                        select new CartProductsView
                        {
                            CartId = cp.Id,
                            CartQuantity = cp.Quantity,
                            CartDateCreated = cp.DateCreated,
                            CartSessionId = cp.SessionId,
                            MenuName = m.Name,
                            MenuDescription = m.Description,
                            ProductId = p.Id,
                            ProductName = p.Name,
                            ProductDescription = p.Description,
                            ProductPrice = p.Price,
                            ProductImageURL = p.ImageURL,
                            ProductRating = p.ProductRating,
                            ProductDiscounts = p.Discounts,
                            UnitName = u.Name,
                            UnitCode = u.Code
                        };
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(int Id, int Quantity)
        {
            var model = _context.CartProducts.Where(m => m.Id == Id).FirstOrDefault();
            model.Quantity = Quantity;
            _context.CartProducts.Update(model);
            _context.SaveChanges();
            return Json(model);
        }

        public ActionResult PlaceOrder(string Payment, string CustomerRequest)
        {
            Orders orders = new Orders();
            orders.Payment = Payment;
            orders.CustomerRequest = CustomerRequest;
            orders.DateCreated = DateTime.Now;
            _context.Orders.Add(orders);
            _context.SaveChanges();

            decimal TotalAmount = 0;
            var OrderId = orders.Id;
            var SessionId = HttpContext.Session.GetString("SessionId");
            var CP = _context.CartProducts.Where(m => m.SessionId == SessionId).ToList();
            foreach (var item in CP)
            {
                OrderProducts OP = new OrderProducts();
                OP.Quantity = item.Quantity;
                OP.OrderId = OrderId;
                OP.ProductId = item.ProductId;
                OP.Price = _context.Products.Find(item.ProductId).Price;
                OP.Discounts = _context.Products.Find(item.ProductId).Discounts;
                _context.OrderProducts.Add(OP);
                _context.SaveChanges();

                var DiscountedPrice = (OP.Price - (OP.Price * OP.Discounts) / 100) * OP.Quantity;
                TotalAmount = TotalAmount + Convert.ToDecimal(DiscountedPrice);
            }

            var O = _context.Orders.Where(m => m.Id == OrderId).FirstOrDefault();
            O.ReferenceNo = "#SKS" + OrderId;
            O.TotalAmount = TotalAmount;
            O.IsPaid = false;
            O.Discounts = 0;
            _context.Orders.Update(O);
            _context.SaveChanges();


            foreach (var item in CP)
            {
                _context.CartProducts.Remove(item);
                _context.SaveChanges();
            }

            return Json(O.ReferenceNo);
        }

        

        [HttpPost]
        public ActionResult Delete(CartProducts model)
        {
            _context.CartProducts.Remove(model);
            _context.SaveChanges();
            return Json(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
