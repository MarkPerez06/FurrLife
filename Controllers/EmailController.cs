using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using FurrLife.Models;

namespace FurrLife.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Email model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fromAddress = new MailAddress("pleasedonotreply.ph@gmail.com", "Do Not Reply");
                    var toAddress = new MailAddress(model.ToEmail);
                    const string fromPassword = "DoNotReply!@#123";

                    //using (var smtp = new SmtpClient
                    //{
                    //    Host = "smtp.gmail.com",
                    //    Port = 587,
                    //    EnableSsl = true,
                    //    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //    UseDefaultCredentials = false,
                    //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    //})

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.ethereal.email",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("lina.hilpert@ethereal.email", "JV2jDy4bPzj5NQr1u4")
                    };



                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = model.Subject,
                        Body = model.Body
                    })
                    {
                        smtp.Send(message);
                        ViewBag.Message = "Email sent successfully!";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message;
                }
            }

            return View(model);
        }
    }

}
