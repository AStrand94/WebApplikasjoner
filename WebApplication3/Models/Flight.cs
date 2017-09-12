using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public virtual List<Customer> Passangers { get; set; }
        public virtual Route Route { get; set; }
        public virtual Airplane Airplane { get; set; }
    }
}