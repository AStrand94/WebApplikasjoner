using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}