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
        public IHttpActionResult RegForm(UserModel um)
        {
            AccountDBEntities adb = new AccountDBEntities();
            adb.Users.Add(new User()
            {
                Email= um.Email,
                MobileNumber = um.MobileNumber,
                FirstName = um.FirstName,
                LastName = um.LastName,
                DateOfBirth = um.DateOfBirth,
                Gender = um.Gender,
                Password = um.Password,
            });
            adb.SaveChanges();
            return Ok();
        }

        public HttpResponseMessage Register (Domain.UserModel um)
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

        public HttpResponseMessage Login (Domain. um)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
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
