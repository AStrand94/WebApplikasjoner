using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Route> Routes { get; set; }
    }
}