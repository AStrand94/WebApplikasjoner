using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public double Price { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public virtual Route Route { get; set; }
        public virtual Airplane Airplane { get; set; }
    }
}