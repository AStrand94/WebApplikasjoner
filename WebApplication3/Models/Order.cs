using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Order
    {
        public List<Flight> Flights { get; set; }
        public Customer Customer { get; set; }

        public Order()
        {
            Flights = new List<Flight>();
        }

    }
}