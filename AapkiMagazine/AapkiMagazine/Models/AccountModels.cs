using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.ObjectModel;
using DataAnnotationsExtensions;
namespace AapkiMagazine.Models
{

 
    public class LogOnModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [Display(Name = "User Email address")]
        [Email]
        public string UserEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [Display(Name = "New Email address")]
        [Email]
        public string Email_New { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password_New { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword_New { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]       
        [Display(Name = "Reset Email address")]
        [Email]
        public string Email_Reset { get; set; }

    }

   
}
