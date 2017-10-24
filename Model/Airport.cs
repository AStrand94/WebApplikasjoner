using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Airport
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public virtual List<Route> Routes { get; set; }
    }
}