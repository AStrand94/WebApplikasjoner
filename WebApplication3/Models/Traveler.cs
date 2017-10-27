using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    [ExcludeFromCodeCoverage]
    public class Traveler
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public int Age { get; set; }
    }
}