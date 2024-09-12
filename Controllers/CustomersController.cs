using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public CustomersController(ApplicationDbContext context, ILogger<CustomersController> logger, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var Persons = _context.Persons.Where(m => m.Email == User.Identity.Name && (m.IsAdmin == true || m.IsStaff == true)).FirstOrDefault();
            if (Persons != null)
            {
                List<IdentityUser> Users = _signInManager.UserManager.Users.ToList();
                ViewBag.Users = new SelectList(Users, "Id", "Email");
                IEnumerable<Persons> model = _context.Persons.OrderByDescending(m => m.Id).Where(m => m.IsMember == true).ToList();
                ViewBag.Persons = Persons;
                return View(model);
            }
            else
            {
                return Redirect("~/Dashboard");
            }
        }

        [HttpPost]
        public ActionResult Save(Persons model)
        {
            if (ModelState.IsValid)
            {
                var Users = _signInManager.UserManager.Users.Where(m => m.Id == model.UserId).FirstOrDefault();
                if (model.Id != 0)
                {
                    var Person = _context.Persons.Where(m => m.Id == model.Id).FirstOrDefault();
                    Person.Email = Users.Email;
                    Person.UserId = Users.Id;
                    Person.CardNumber = model.CardNumber;
                    Person.PIN = model.PIN;
                    Person.FirstName = model.FirstName;
                    Person.MiddleName = model.MiddleName;
                    Person.LastName = model.LastName;
                    Person.Birthday = model.Birthday;
                    Person.CompleteAddress = model.CompleteAddress;
                    Person.ContactNumber = model.ContactNumber;
                    Person.Gender = model.Gender;
                    Person.SeniorCitizenID = model.SeniorCitizenID;
                    Person.PWDID = model.PWDID;

                    if (model.SeniorCitizenID != null && model.SeniorCitizenID != "")
                    {
                        Person.IsSeniorCitizen = true;
                    }
                    else
                    {
                        Person.IsSeniorCitizen = false;
                    }
                    if (model.PWDID != null && model.PWDID != "")
                    {
                        Person.IsPWD = true;
                    }
                    else
                    {
                        Person.IsPWD = false;
                    }
                    _context.Persons.Update(Person);
                }
                else
                {
                    var Person = _context.Persons.Where(m => m.UserId == model.UserId).ToList();
                    if (Person.Count == 0)
                    {
                        model.Email = Users.Email;
                        model.UserId = model.UserId;
                        model.IsAdmin = false;
                        model.IsStaff = false;
                        model.IsMember = true;
                        model.DateCreated = DateTime.Now;
                        _context.Persons.Add(model);
                    }
                    else
                    {
                        return Json(new { success = false, message = "User already exists!" });
                    }
                }
                _context.SaveChanges();
                return Json(new { success = true, message = "Form submitted successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public ActionResult Edit(Persons model)
        {
            var data = _context.Persons.Where(m => m.Id == model.Id).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public ActionResult Delete(Persons model)
        {
            _context.Persons.Remove(model);
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
