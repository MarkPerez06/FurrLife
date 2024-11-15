using FurrLife.Data;
using FurrLife.Models;
using FurrLife.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [Route("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        [Route("Chat")]
        public IActionResult Chat(int AppointmentId)
        {
            var Username = "";
            var model = _context.Appointments.Where(m => m.Id == AppointmentId).FirstOrDefault();
            if (model != null)
            {
                var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
                if (user != null && user.SecurityStamp == UserRoles.Customer.Id)
                {
                    var vet = _context.Users.Where(m => m.Id == model.UserId).FirstOrDefault();
                    if (vet != null)
                    {
                        Username = vet.UserName;
                    }
                }

                if (user != null && user.SecurityStamp != UserRoles.Customer.Id && model.CusUserId != "")
                {
                    var cus = _context.Users.Where(m => m.Id == model.CusUserId).FirstOrDefault();
                    if (cus != null)
                    {
                        Username = cus.UserName;
                    }
                }

            }
            ViewBag.Username = Username;
            return View();
        }

        [Route("Consultation")]
        public IActionResult Consultation()
        {
            return View();
        }

        public IActionResult Index()
        {
            List<IdentityUser> vetUsers = _context.Users.Where(m => m.SecurityStamp == UserRoles.Veterinarian.Id).ToList();
            ViewBag.vetUsers = vetUsers;
            

            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            var model = _context.Appointments.ToList();

            if (user.SecurityStamp == UserRoles.Veterinarian.Id)
            {
                model = _context.Appointments.Where(m => m.UserId == user.Id).ToList();
            }

            if (user.SecurityStamp == UserRoles.Customer.Id)
            {
                model = _context.Appointments.Where(m => m.CusUserId == user.Id).ToList();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();

            if (user.SecurityStamp == UserRoles.Customer.Id)
            {
                var schedules = _context.Appointments.Where(m => m.CusUserId == user.Id)
                .Select(a => new
                {
                    id = a.Id.ToString(),
                    calendarId = "1",
                    title = a.Title,
                    category = a.IsAllDay ? "allday" : "time",
                    start = a.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = a.End.ToString("yyyy-MM-ddTHH:mm:ss"),
                    isAllDay = a.IsAllDay
                })
                .ToList();
                return Json(schedules);
            }
            else if (user.SecurityStamp == UserRoles.Veterinarian.Id)
            {
                var schedules = _context.Appointments.Where(m => m.UserId == user.Id)
                .Select(a => new
                {
                    id = a.Id.ToString(),
                    calendarId = "1",
                    title = a.Title,
                    category = a.IsAllDay ? "allday" : "time",
                    start = a.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = a.End.ToString("yyyy-MM-ddTHH:mm:ss"),
                    isAllDay = a.IsAllDay
                })
                .ToList();
                return Json(schedules);
            }
            else
            {
                var schedules = _context.Appointments.Where(m => m.CusUserId != "")
                .Select(a => new
                {
                    id = a.Id.ToString(),
                    calendarId = "1",
                    title = a.Title,
                    category = a.IsAllDay ? "allday" : "time",
                    start = a.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = a.End.ToString("yyyy-MM-ddTHH:mm:ss"),
                    isAllDay = a.IsAllDay
                })
                .ToList();
                return Json(schedules);
            }
        }


        [HttpPost]
        public ActionResult Save(Appointments model)
        {
            if (model.Id != 0)
            {
                var appointment = _context.Appointments.Where(m => m.Id == model.Id).FirstOrDefault();
                appointment.Title = model.Title;
                appointment.Start = model.Start;
                appointment.End = model.End;
                appointment.IsAllDay = model.IsAllDay;
                appointment.UserId = model.UserId;
                _context.Appointments.Update(appointment);
            }
            else
            {
                model.CalendarId = 1;
                model.Category = model.IsAllDay ? "allday" : "time";
                model.CusUserId = "";
                model.OrderId = 0;
                _context.Appointments.Add(model);
            }
            _context.SaveChanges();
            return Json(new { success = true, message = "Form submitted successfully!" });
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
