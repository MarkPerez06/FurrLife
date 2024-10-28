using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ILogger<DiscountsController> _logger;
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context, ILogger<DiscountsController> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            var schedules = _context.Appointments
                .Select(a => new
                {
                    id = a.Id.ToString(), // Convert Id to string
                    calendarId = "1", // Assuming a static calendarId; modify as needed
                    title = a.Title + "("+ a.Start.ToString("hh:mm tt") + " - "+ a.End.ToString("hh:mm tt") + ")",
                    category = a.IsAllDay ? "allday" : "time", // Set category based on IsAllDay
                    start = a.Start.ToString("yyyy-MM-ddTHH:mm:ss"), // Format StartDate
                    end = a.End.ToString("yyyy-MM-ddTHH:mm:ss"),     // Format EndDate
                    isAllDay = a.IsAllDay
                })
                .ToList();

            return Json(schedules); // Return as JSON
        }


        [HttpPost]
        public ActionResult Save(Appointments model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Appointments.Update(model);
                }
                else
                {
                    _context.Appointments.Add(model);
                }
                _context.SaveChanges();
                return Json(new { success = true, message = "Form submitted successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public ActionResult Edit(Appointments model)
        {
            var data = _context.Appointments.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Appointments model)
        {
            _context.Appointments.Remove(model);
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
