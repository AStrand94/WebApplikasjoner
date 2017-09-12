using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Route
    {
        public int Id { get; set; }
        public virtual Airport ToAirport { get; set; }
        public virtual Airport FromAirport { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
}