using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Registration.Models;
using Registration.Crypto;
using Newtonsoft.Json;
using System.Text;

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegisterViewModel rv)
        {
            if (ModelState.IsValid)
            {
                //HttpClient Client = new HttpClient();
                //Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"].ToString());
                //HttpResponseMessage response = Client.PutResponse("api/Users/Registration", rv);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"].ToString());

                    //HTTP POST
                    Domain.UserModel um = new Domain.UserModel()
                    {
                        MobileNumber = rv.MobileNumber,
                        FirstName = rv.FirstName,
                        LastName = rv.LastName,
                        DateOfBirth = rv.DateOfBirth,
                        Gender = rv.Gender,
                        Email = rv.Email,
                        Password = Crypto.Crypto.Hash(rv.Password)
                    };
                    var stringContent = new StringContent(JsonConvert.SerializeObject(um), Encoding.UTF8, "application/json");
                    var response = client.PostAsync("api/Users/Register", stringContent);
                    //var postTask = client.PostAsync("api/Users/Registration", um);
                    response.Wait();

                    var result = response.Result;
                    if(result.ReasonPhrase == "Created")
                    {
                        ViewBag.Status = result.ReasonPhrase;
                    }
                    else
                    {
                        ViewBag.Status = "Mobile Phone or Email already exists";
                    }
                    
                }

                //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel lv)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"].ToString());

                    //HTTP POST
                    Domain.UserLogin ul = new Domain.UserLogin()
                    {
                        Email = lv.Email,
                        Password = Crypto.Crypto.Hash(lv.Password)
                    };
                    var stringContent = new StringContent(JsonConvert.SerializeObject(ul), Encoding.UTF8, "application/json");
                    var response = client.PostAsync("api/Users/Login", stringContent);
                    response.Wait();

                    var result = response.Result;
                    ViewBag.Message = result.ReasonPhrase;
                }

            }
            return View();
        }
    }
}