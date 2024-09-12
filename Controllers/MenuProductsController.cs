using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using static FurrLife.Controllers.DashboardController;

namespace FurrLife.Controllers
{
    public class MenuProductsController : Controller
    {
        private readonly ILogger<MenuProductsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public MenuProductsController(ApplicationDbContext context, ILogger<MenuProductsController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var Persons = _context.Persons.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            if (Persons != null)
            {
                ViewBag.Persons = Persons;
            }
            else
            {
                Persons Model = new Persons();
                Model.IsAdmin = false;
                Model.IsStaff = false;
                Model.IsMember = true;
                ViewBag.Persons = Model;
            }

            List<Discounts> discounts = _context.Discounts.Where(m => m.IsActive == true).OrderBy(m => m.Percentage).ToList();
            ViewBag.Discounts = new SelectList(discounts, "Percentage", "Name");

            List<Units> units = _context.Units.Where(m => m.IsActive == true).OrderBy(m => m.Name).ToList();
            ViewBag.Units = new SelectList(units, "Id", "Name");

            List<Menu> menu = _context.Menu.Where(m => m.IsActive == true).OrderBy(m => m.Name).ToList();
            ViewBag.Menu = new SelectList(menu, "Id", "Name");

            var products = _context.Products.OrderByDescending(m => m.Price).FirstOrDefault();
            if (products != null)
            {
                ViewBag.Products = products.Price;
                ViewBag.TopProductPrice = products.Price;
            }
            else
            {
                ViewBag.Products = 100;
                ViewBag.TopProductPrice = 0;
            }
            return View(menu);
        }

        [HttpPost]
        public IActionResult CartProducts(CartProducts model)
        {
            if (HttpContext.Session.GetString("SessionId") == null)
            {
                HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());
            }

            if (HttpContext.Session.GetString("SessionId") != null)
            {
                model.SessionId = HttpContext.Session.GetString("SessionId");
                model.DateCreated = DateTime.Now;

                var FilterCartProducts = _context.CartProducts.Where(m => m.ProductId == model.ProductId && m.SessionId == model.SessionId).ToList();
                if (FilterCartProducts.Count == 0)
                {
                    _context.CartProducts.Add(model);
                }
                if (FilterCartProducts.Count > 0)
                {
                    var GetQuantity = _context.CartProducts.Where(m => m.ProductId == model.ProductId && m.SessionId == model.SessionId).FirstOrDefault();
                    GetQuantity.Quantity = GetQuantity.Quantity + 1;
                    _context.CartProducts.Update(GetQuantity);
                }
                _context.SaveChanges();
            }
            return Json(model);
        }

        public IActionResult TotalAddedCart()
        {
            var Total = 0;

            if (HttpContext.Session.GetString("SessionId") != null)
            {
                Total = _context.CartProducts.Where(m => m.SessionId == HttpContext.Session.GetString("SessionId")).ToList().Count;
            }
            return Json(Total);
        }

        public IActionResult _AddedToCartProducts()
        {
            var model = from cp in _context.CartProducts
                        join p in _context.Products
                        on cp.ProductId equals p.Id
                        join m in _context.Menu
                        on p.MenuId equals m.Id
                        join u in _context.Units
                        on p.UnitId equals u.Id
                        where cp.SessionId == HttpContext.Session.GetString("SessionId") orderby cp.Id descending
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

            return PartialView("_AddedToCartProducts", model);
        }

        public IActionResult _Menu()
        {
            var OrderProducts = _context.OrderProducts.ToList();
            if (OrderProducts.Count > 0)
            {
                ViewBag.HasOrderProducts = true;
            }
            else
            {
                ViewBag.HasOrderProducts = false; ;
            }
            IEnumerable<Menu> model = _context.Menu.Where(m => m.IsActive == true).OrderBy(m => m.Name).ToList();
            return PartialView("_Menu", model);
        }

        public IActionResult _Discounts()
        {
            IEnumerable<Discounts> model = _context.Discounts.Where(m => m.IsActive == true).OrderBy(m => m.Percentage).ToList();
            return PartialView("_Discounts", model);
        }
        
        public IActionResult _ProductsAll()
        {
            IEnumerable<Products> modelProducts = _context.Products.Where(m=>m.IsActive == true).OrderBy(m => m.Name).ToList();
            return PartialView("_Products", modelProducts);
        }

        public IActionResult _ProductsBestSeller()
        {
            var TopProducts = from o in _context.Orders
                              join op in _context.OrderProducts on o.Id equals op.OrderId
                              join p in _context.Products on op.ProductId equals p.Id
                              where o.IsPaid == true
                              group new { op, p } by new { p.Id, p.Name } into g
                              orderby g.Sum(item => item.op.Quantity) descending
                              select new Top5Products
                              {
                                  Id = g.Key.Id,
                                  Name = g.Key.Name,
                                  Count = g.Sum(item => item.op.Quantity)
                              };

            var top10 = TopProducts.Take(9).ToList();

            // Get the top 10 product IDs from the best sellers
            var top10ProductIds = top10.Select(p => p.Id).ToList();

            // Select the top 10 products from modelProducts using the top10ProductIds
            IEnumerable<Products> top10Products = _context.Products.Where(m => m.IsActive == true && top10ProductIds.Contains(m.Id)).ToList();


            foreach (var item in top10Products)
            {
                item.ProductRating = 5;
                _context.Products.Update(item);
                _context.SaveChanges();
            }

            return PartialView("_Products", top10Products);
        }
        public IActionResult _Products(int MenuId)
        {
            IEnumerable<Products> modelProducts = _context.Products.Where(m => m.IsActive == true && m.MenuId == MenuId).OrderBy(m => m.Name).ToList();
            return PartialView("_Products", modelProducts);
        }

        public IActionResult _ProductsFilter(int MenuId, int Price, int Discounts, int ProductRating)
        {
            IEnumerable<Products> model = _context.Products.Where(m => m.MenuId == 0).ToList();

            if (MenuId > 0 && Price > 0 && Discounts > 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.MenuId == MenuId && m.Price <= Price && m.Discounts == Discounts && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId > 0 && Price > 0 && Discounts > 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.MenuId == MenuId && m.Price <= Price && m.Discounts == Discounts).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId > 0 && Price > 0 && Discounts == 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.MenuId == MenuId && m.Price <= Price && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId > 0 && Price > 0 && Discounts == 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.MenuId == MenuId && m.Price <= Price).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }


            else if (MenuId == 0 && Price > 0 && Discounts > 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.Price <= Price && m.Discounts == Discounts && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId == 0 && Price > 0 && Discounts > 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.Price <= Price && m.Discounts == Discounts).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId == 0 && Price > 0 && Discounts == 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.Price <= Price && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId == 0 && Price > 0 && Discounts == 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.IsActive == true && m.Price <= Price).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }

            return PartialView("_Products", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Products model, IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Products");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                model.ImageURL = "/Products/" + uniqueFileName;
            }

            model.DateCreated = DateTime.Now;
            if (model.Id != 0)
            {
                _context.Products.Update(model);
            }
            else
            {
                _context.Products.Add(model);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Edit(Products model)
        {
            var data = _context.Products.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Products model)
        {
            _context.Products.Remove(model);
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
