﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class Flight
    {
        public int Id { get; set; }

        [Display(Name = "Flight date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Required]
        [Range(0,9999999)]
        public double Price { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

        [Required]
        public virtual Route Route { get; set; }
        
        [Required]
        public virtual Airplane Airplane { get; set; }

        public string FullInfo
        {
            get
            {
                return String.Format("{0} - {1}, {2}", Route.FromAirport.Name, Route.ToAirport.Name, Time);
            }
        }
    }
}