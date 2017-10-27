using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class Airplane
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Seats can not be 0")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a number")]
        public int Seats { get; set; }

        public virtual List<Flight> Flights { get; set; }
    }
}