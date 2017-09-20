using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Order
    {
        public virtual Flight Flight { get; set; }
        public virtual Customer Customer { get; set; }

    }
}