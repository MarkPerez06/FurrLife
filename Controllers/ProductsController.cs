using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using FurrLife.Static;

namespace FurrLife.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null && user.SecurityStamp == UserRoles.Administrator.Id)
            {
                return Redirect("~/Dashboard");
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

        public IActionResult _Menu()
        {
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
            IEnumerable<Products> modelProducts = _context.Products.OrderBy(m => m.Name).ToList();
            return PartialView("_Products", modelProducts);
        }

        public IActionResult _Products(int MenuId)
        {
            IEnumerable<Products> modelProducts = _context.Products.Where(m => m.MenuId == MenuId).OrderBy(m => m.Name).ToList();
            return PartialView("_Products", modelProducts);
        }

        public IActionResult _ProductsFilter(int MenuId, int Price, int Discounts, int ProductRating)
        {
            IEnumerable<Products> model = _context.Products.Where(m => m.MenuId == 0).ToList();

            if (MenuId > 0 && Price > 0 && Discounts > 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.MenuId == MenuId && m.Price <= Price && m.Discounts == Discounts && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId > 0 && Price > 0 && Discounts > 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.MenuId == MenuId && m.Price <= Price && m.Discounts == Discounts).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId > 0 && Price > 0 && Discounts == 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.MenuId == MenuId && m.Price <= Price && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId > 0 && Price > 0 && Discounts == 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.MenuId == MenuId && m.Price <= Price).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }


            else if (MenuId == 0 && Price > 0 && Discounts > 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.Price <= Price && m.Discounts == Discounts && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId == 0 && Price > 0 && Discounts > 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.Price <= Price && m.Discounts == Discounts).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId == 0 && Price > 0 && Discounts == 0 && ProductRating > 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.Price <= Price && m.ProductRating == ProductRating).OrderBy(m => m.Name).ToList();
                model = modelFilter;
            }
            else if (MenuId == 0 && Price > 0 && Discounts == 0 && ProductRating == 0)
            {
                IEnumerable<Products> modelFilter = _context.Products.Where(m => m.Price <= Price).OrderBy(m => m.Name).ToList();
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
                // Validate file type and size if needed
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Image", "Invalid file type. Please upload an image file.");
                    return View(model);
                }

                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Products");

                // Ensure the folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error uploading the file: " + ex.Message);
                    return View(model);
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
