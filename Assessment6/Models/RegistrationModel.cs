using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Assessment6.Models
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Attending { get; set; }

        //Optional: (depends on whether or not they're attending party)
        public string PartyDate { get; set; }
        public string PlusOne { get; set; }
        public string PlusOneName { get; set; }
    }
}