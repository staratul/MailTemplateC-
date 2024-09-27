using EmailTemplate.Models;
using EmailTemplate.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly MailService _mailService;

        public HomeController()
        {
            _mailService = new MailService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(FormCollection form)
        {
            dynamic formData = new ExpandoObject();
            formData.Name = form["Name"];
            formData.Email = form["Email"];
            formData.Subject = form["Subject"];
            formData.Message = form["Message"];

            _mailService.SendContactUsEmail(formData);
            ViewBag.Message = "Your message has been sent successfully!";

            return View("Contact");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}