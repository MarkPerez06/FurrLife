﻿using FurrLife.Data;
using FurrLife.Hubs;
using FurrLife.Models;
using FurrLife.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        public AppointmentsController(ApplicationDbContext context, ILogger<AppointmentsController> logger, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _logger = logger;
            _hubContext = hubContext;

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

        [HttpGet]

        public IActionResult _ChatDetails(int AppointmentId)
        {
            List<IdentityUser> users = _context.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            var model = _context.Messages.Where(m => m.AppointmentId == AppointmentId).OrderByDescending(m => m.DateCreated).ToList();
            return PartialView("_ChatDetails", model);
        }

        [HttpPost]
        public ActionResult SendChat(int AppointmentId, string Message)
        {
            if (AppointmentId != 0)
            {
                var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();

                var appointment = _context.Appointments.Where(m => m.Id == AppointmentId).FirstOrDefault();

                Messages messages = new Messages();
                messages.AppointmentId = AppointmentId;
                messages.Message = Message;
                messages.DateCreated = DateTime.Now;
                if (user != null && user.SecurityStamp == UserRoles.Customer.Id)
                {
                    messages.CustId = user.Id;
                    messages.UserId = "";
                }

                if (user != null && user.SecurityStamp != UserRoles.Customer.Id)
                {
                    messages.CustId = "";
                    messages.UserId = user.Id;
                }

                _context.Messages.Add(messages);
            }
            _context.SaveChanges();


            _hubContext.Clients.All.SendAsync("RefreshChat");
            return Json(new { success = true, message = "Form submitted successfully!" });
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(int AppointmentId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var model = new Messages();

            // Check if the file is a PDF or an image
            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("Unsupported file type.");

            // Create the ApplicationFormRequirements directory if it doesn't exist
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ChatUploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);


            // Create a new file name with GUID appended
            var uniqueFileName = $"{model.AppointmentId + Guid.NewGuid().ToString()}{fileExtension}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (AppointmentId != 0)
            {
                var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();

                var appointment = _context.Appointments.Where(m => m.Id == AppointmentId).FirstOrDefault();

                Messages messages = new Messages();
                messages.AppointmentId = AppointmentId;
                messages.Message = "/ChatUploads/" + uniqueFileName;
                messages.DateCreated = DateTime.Now;
                if (user != null && user.SecurityStamp == UserRoles.Customer.Id)
                {
                    messages.CustId = user.Id;
                    messages.UserId = "";
                }

                if (user != null && user.SecurityStamp != UserRoles.Customer.Id)
                {
                    messages.CustId = "";
                    messages.UserId = user.Id;
                }

                _context.Messages.Add(messages);
            }
            _context.SaveChanges();
            await _hubContext.Clients.All.SendAsync("RefreshChat");
            return RedirectToAction("Chat", new { AppointmentId });
        }
        

        [Route("Consultation")]
        public IActionResult Consultation()
        {
            List<IdentityUser> vetUsers = _context.Users.Where(m => m.SecurityStamp == UserRoles.Veterinarian.Id).ToList();
            ViewBag.vetUsers = vetUsers;


            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            var model = _context.Appointments.Where(m => m.CalendarId == 2).OrderByDescending(m => m.End).ToList();

            if (user.SecurityStamp == UserRoles.Veterinarian.Id)
            {
                model = _context.Appointments.Where(m => m.UserId == user.Id && m.CalendarId == 2).OrderByDescending(m => m.End).ToList();
            }

            if (user.SecurityStamp == UserRoles.Customer.Id)
            {
                model = _context.Appointments.Where(m => m.CusUserId == user.Id && m.CalendarId == 2).OrderByDescending(m => m.End).ToList();
            }

            List<Appointments> openSchedule = _context.Appointments.Where(m => m.CusUserId == "" && m.CalendarId == 2 && m.End >= DateTime.Now).ToList();
            ViewBag.openSchedule = openSchedule;
            return View(model);
        }

        


        [Route("OnlineConsultation")]
        public IActionResult OnlineConsultation(int ConsultationId)
        {
            return View();
        }

        public IActionResult Index()
        {
            List<IdentityUser> vetUsers = _context.Users.Where(m => m.SecurityStamp == UserRoles.Veterinarian.Id).ToList();
            ViewBag.vetUsers = vetUsers;


            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            var model = _context.Appointments.Where(m => m.CalendarId == 1).OrderByDescending(m => m.End).ToList();

            if (user.SecurityStamp == UserRoles.Veterinarian.Id)
            {
                model = _context.Appointments.Where(m => m.UserId == user.Id && m.CalendarId == 1).OrderByDescending(m => m.End).ToList();
            }

            if (user.SecurityStamp == UserRoles.Customer.Id)
            {
                model = _context.Appointments.Where(m => m.CusUserId == user.Id && m.CalendarId == 1).OrderByDescending(m => m.End).ToList();
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
                    calendarId = a.CalendarId,
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
                    calendarId = a.CalendarId,
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
                    calendarId = a.CalendarId,
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
        public ActionResult AddConsultation(int ScheduleId)
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();

            if (ScheduleId > 0)
            {
                var appointment = _context.Appointments.Where(m => m.Id == ScheduleId).FirstOrDefault();
                appointment.CusUserId = user.Id;
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
            }
            return Json(user);
        }


        [HttpPost]
        public ActionResult Save(Appointments model)
        {
            if (model.Id != 0)
            {
                var appointment = _context.Appointments.Where(m => m.Id == model.Id).FirstOrDefault();
                if (model.CalendarId == 1)
                {
                    appointment.Title = "Store Appointment";
                }
                else
                {
                    appointment.Title = "Online Consultation";
                }
                appointment.CalendarId = model.CalendarId;
                appointment.Start = model.Start;
                appointment.End = model.End;
                appointment.IsAllDay = model.IsAllDay;
                appointment.UserId = model.UserId;
                _context.Appointments.Update(appointment);
            }
            else
            {
                if (model.CalendarId == 1)
                {
                    model.Title = "Store Appointment";
                }
                else
                {
                    model.Title = "Online Consultation";
                }
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
