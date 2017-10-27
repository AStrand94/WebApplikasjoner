using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class Airport
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z .&'-]+)$", ErrorMessage = "Invalid name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid code")]
        public string Code { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid country")]
        public string Country { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid city")]
        public string City { get; set; }

        public virtual List<Route> Routes { get; set; }
    }
}