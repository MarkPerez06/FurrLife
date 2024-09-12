using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;

namespace FurrLife.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IPasswordHasher<IdentityUser> _passwordHasher;
        public UsersController(ApplicationDbContext context, ILogger<UsersController> logger, SignInManager<IdentityUser> signInManager, IPasswordHasher<IdentityUser> passwordHasher)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            var Persons = _context.Persons.Where(m => m.Email == User.Identity.Name && (m.IsAdmin == true || m.IsStaff == true)).FirstOrDefault();
            if (Persons != null)
            {
                IEnumerable<IdentityUser> model = _signInManager.UserManager.Users.ToList();
                ViewBag.Persons = Persons;

                List<Users> users = new List<Users>();

                foreach (var item in model)
                {
                    users.Add(new Users { Id = item.Id, UserName = item.UserName, Email = item.Email, EmailConfirmed = item.EmailConfirmed, Password = item.PasswordHash });
                }
                return View(users);
            }
            else
            {
                return Redirect("~/Dashboard");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(string Id, string Username, string Password, string ConfirmPassword, string EmailConfirmed)
        {
            IdentityUser user = await _signInManager.UserManager.FindByIdAsync(Id);
            if (user != null)
            {
                if (EmailConfirmed == "True")
                {
                    user.EmailConfirmed = true;
                }
                else
                {
                    user.EmailConfirmed = false;
                }
               
                if ((Password != "" && Password != null) && (ConfirmPassword != "" && ConfirmPassword != null))
                {
                    if (Password == ConfirmPassword)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, Password);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Password and Confirm Password doesn't match!" });
                    }
                }
                var model = await _signInManager.UserManager.UpdateAsync(user);

                return Json(new { success = true, message = "User was successfully updated!" });

            }
            else
            {
                return Json(new { success = false, message = "User doesn't exists!" });
            }
        }
    }
}
