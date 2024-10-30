using FurrLife.Data;
using FurrLife.Models;
using FurrLife.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context, ILogger<AppointmentsController> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IActionResult Index()
        {
            List<IdentityUser> users = _context.Users.Where(m => m.SecurityStamp == UserRoles.Veterinarian.Id).ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            var model = _context.Schedule.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Schedule model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Schedule.Update(model);
                }
                else
                {
                    _context.Schedule.Add(model);
                }
                _context.SaveChanges();
                return Json(new { success = true, message = "Form submitted successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public ActionResult Edit(Schedule model)
        {
            var data = _context.Schedule.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Schedule model)
        {
            _context.Schedule.Remove(model);
            _context.SaveChanges();
            return Json(model);
        }


        [Route("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            var schedules = _context.Appointments
                .Select(a => new
                {
                    id = a.Id.ToString(),
                    calendarId = "1",
                    title = a.Title + " (" + a.Start.ToString("hh:mm tt") + " - " + a.End.ToString("hh:mm tt") + ")",
                    category = a.IsAllDay ? "allday" : "time",
                    start = a.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = a.End.ToString("yyyy-MM-ddTHH:mm:ss"),
                    isAllDay = a.IsAllDay
                })
                .ToList();
            return Json(schedules);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
