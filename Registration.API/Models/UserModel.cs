using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Registration.API.Models
{
    public class UserModel
    {
        public string MobileNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public System.DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string RePassword { get; set; }
    }
}