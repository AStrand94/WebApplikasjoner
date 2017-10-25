using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Route
    {
        public int Id { get; set; }
        
        [Required]
        public virtual Airport ToAirport { get; set; }

        public virtual Airport FromAirport { get; set; }

        public virtual List<Flight> Flights { get; set; }

        [Required]
        public virtual TimeSpan FlightTime { get; set; }
    }
}