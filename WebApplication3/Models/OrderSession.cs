using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class OrderSession
    {
        public List<int> Flights { get; set; }
        public Customer Customer { get; set; }
        public List<Customer> Travelers { get; set; }
        public int NumberTravellers { get; set; }
        public double TotalPrice { get; set; }

        public OrderSession()
        {
            Flights = new List<int>();
        }

    }
}