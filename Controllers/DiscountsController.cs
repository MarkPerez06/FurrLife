using FurrLife.Data;
using FurrLife.Models;
using FurrLife.Static;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class DiscountsController : Controller
    {
        private readonly ILogger<DiscountsController> _logger;
        private readonly ApplicationDbContext _context;

        public DiscountsController(ApplicationDbContext context, ILogger<DiscountsController> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IActionResult Index()
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null || user.SecurityStamp != UserRoles.Administrator.Id)
            {
                return Redirect("~/MenuProducts");
            }
            IEnumerable<Discounts> model = _context.Discounts.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Discounts model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Discounts.Update(model);
                }
                else
                {
                    model.DateCreated = DateTime.Now;
                    _context.Discounts.Add(model);
                }
                _context.SaveChanges();
                return Json(new { success = true, message = "Form submitted successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public ActionResult Edit(Discounts model)
        {
            var data = _context.Discounts.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Discounts model)
        {
            _context.Discounts.Remove(model);
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
