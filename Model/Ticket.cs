using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Ticket
    {

        public int Id { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Flight Flight { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z .&'-]+)$", ErrorMessage = "Invalid first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z .&'-]+)$", ErrorMessage = "Invalid last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}