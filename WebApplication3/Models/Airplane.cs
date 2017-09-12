using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Seats { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
}