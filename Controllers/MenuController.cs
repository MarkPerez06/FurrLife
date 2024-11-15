using FurrLife.Data;
using FurrLife.Models;
using FurrLife.Static;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context, ILogger<MenuController> logger)
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
            IEnumerable<Menu> model = _context.Menu.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Menu model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Menu.Update(model);
                }
                else
                {
                    model.DateCreated = DateTime.Now;
                    _context.Menu.Add(model);
                }
                _context.SaveChanges();
                return Json(new { success = true, message = "Form submitted successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public ActionResult Edit(Menu model)
        {
            var data = _context.Menu.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Menu model)
        {
            _context.Menu.Remove(model);
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
