using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Registration.Models;

namespace Registration.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //public enum Gender
        //{
        //    Male,
        //    Female
        //}
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Registration(RegisterViewModel rv)
        //{
        //    if (ModelState.IsValid)
        //    {
                
        //    }
        //}
    }
}