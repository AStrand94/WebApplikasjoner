using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}