using FurrLife.Data;
using FurrLife.Models;
using FurrLife.Static;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class UnitsController : Controller
    {
        private readonly ILogger<UnitsController> _logger;
        private readonly ApplicationDbContext _context;

        public UnitsController(ApplicationDbContext context, ILogger<UnitsController> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IActionResult Index()
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null && user.SecurityStamp == UserRoles.Administrator.Id)
            {
                return Redirect("~/Dashboard");
            }
            var model = _context.Units.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Units model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Units.Update(model);
                }
                else
                {
                    model.DateCreated = DateTime.Now;
                    _context.Units.Add(model);
                }
                _context.SaveChanges();
                return Json(new { success = true, message = "Form submitted successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public ActionResult Edit(Units model)
        {
            var data = _context.Units.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Units model)
        {
            _context.Units.Remove(model);
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
