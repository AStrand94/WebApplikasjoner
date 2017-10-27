using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z .&'-]+)$", ErrorMessage = "Invalid first name")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [RegularExpression("^([a-zA-Z .&'-]+)$", ErrorMessage = "Invalid last name")]
        [Display(Name = "Last Name")]
        [Required]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11)]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "You must provide a valid supervisor email address.")]
        public string Email { get; set; }

        public virtual List<Order> Order { get; set; }

        public string FullInfo
        {
            get
            {
                return String.Format("{0}, {1}", Lastname, Firstname);
            }
        }
    }
}