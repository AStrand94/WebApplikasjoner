using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Flight
    {
        public int Id { get; set; }

        [Display(Name = "Flight date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        public double Price { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public virtual Route Route { get; set; }
        public virtual Airplane Airplane { get; set; }
    }
}