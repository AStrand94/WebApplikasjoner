using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Customer Traveler { get; set; }
    }
}