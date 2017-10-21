using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        
        [Required(ErrorMessage = "Please enter a valid number!")]
        public int Seats { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
}