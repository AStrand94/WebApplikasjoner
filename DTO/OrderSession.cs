using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WebApplication3.Model;

namespace DTO.Models
{
    [ExcludeFromCodeCoverage]
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