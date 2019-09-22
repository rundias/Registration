using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Registration.API.Models;

namespace Registration.API.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult Register(Domain.UserModel um)
        {
            string message = "";
            try
            {
                using (AccountDBEntities adb = new AccountDBEntities())
                {
                    var isExistEmail = IsEmailExist(um.Email);
                    if (isExistEmail)
                    {
                        message += "Email already exists|";
                    }
                    var isMobileNumber = IsMobileNumberExist(um.MobileNumber);
                    if (isMobileNumber)
                    {
                        message += "Mobile number already exists|";
                    }
                    if (!isExistEmail && !isMobileNumber)
                    {
                        adb.Users.Add(new User()
                        {
                            Email = um.Email,
                            MobileNumber = um.MobileNumber,
                            FirstName = um.FirstName,
                            LastName = um.LastName,
                            DateOfBirth = um.DateOfBirth,
                            Gender = um.Gender,
                            Password = um.Password,
                        });
                        adb.SaveChanges();
                        return Content(HttpStatusCode.Created, um);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, message);
                    }
                }
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

        public IHttpActionResult Login(Domain.UserLogin um)
        {
            //string message = "";
            try
            {
                using (AccountDBEntities adb = new AccountDBEntities())
                {
                    var v = adb.Users.Where(a => a.Email == um.Email).FirstOrDefault();
                    if (v != null)
                    {
                        if (string.Compare(um.Password, v.Password) == 0)
                        {
                            return Content(HttpStatusCode.Accepted, "Login Success");
                        }
                        else
                        {
                            return Content(HttpStatusCode.NotFound, "Wrong Password");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Wrong Email");
                    }
                }
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Register1 (Domain.UserModel um)
        {
            string message = "";
            try
            {
                using (AccountDBEntities adb = new AccountDBEntities())
                {
                    var isExistEmail = IsEmailExist(um.Email);
                    if (isExistEmail)
                    {
                        message += "Email already exists|";
                    }
                    var isMobileNumber = IsMobileNumberExist(um.MobileNumber);
                    if (isMobileNumber)
                    {
                        message += "Mobile number already exists|";
                    }
                    if(!isExistEmail && !isMobileNumber)
                    {
                        adb.Users.Add(new User()
                        {
                            Email = um.Email,
                            MobileNumber = um.MobileNumber,
                            FirstName = um.FirstName,
                            LastName = um.LastName,
                            DateOfBirth = um.DateOfBirth,
                            Gender = um.Gender,
                            Password = um.Password,
                        });
                        adb.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.Created, um);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (AccountDBEntities adb = new AccountDBEntities())
            {
                var v = adb.Users.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public bool IsMobileNumberExist(string mobileNumber)
        {
            using (AccountDBEntities adb = new AccountDBEntities())
            {
                var v = adb.Users.Where(a => a.MobileNumber == mobileNumber).FirstOrDefault();
                return v != null;
            }
        }
    }
}
