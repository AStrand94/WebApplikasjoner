using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using WebApplication3.Model;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class Travel
    {
        public Travel(Flight FromFlight, Flight ToFlight)
        {
            this.ToFlight = ToFlight;
            this.FromFlight = FromFlight;
            isSingleFlight = false;
            AddPrices();
        }

        public Travel(Flight flight)
        {
            FromFlight = flight;
            isSingleFlight = true;
            AddPrices();
        }
        
        public Flight FromFlight { get; }
        public Flight ToFlight { get; }
        public bool isSingleFlight { get; set; }
        public bool isReturnFlight { get; set; }
        public string TravelDescription { get; set; }
        public string TravelFromString { get; set; }
        public string TravelToString { get; set; }
        public double Price { get; set; }

        public void Init()
        {
            CreateStrings();
        }

        private void CreateStrings()
        {
            if (ToFlight == null)
            {
                TravelDescription = "Direct flight:";
                TravelFromString = FromFlight.Route.FromAirport.Name + " - " + FromFlight.Route.ToAirport.Name;
                TravelToString = "";
            }
            else
            {
                TravelDescription = "1 stopover:";
                TravelFromString = FromFlight.Route.FromAirport.Name + " - " + FromFlight.Route.ToAirport.Name;
                TravelToString = ToFlight.Route.FromAirport.Name + " - " + ToFlight.Route.ToAirport.Name;
            }
        }

        private void AddPrices()
        {
            if (ToFlight == null)
            {
                Price = FromFlight.Price;
            }
            else
            {
                Price = FromFlight.Price + ToFlight.Price;
            }
        }
    
    }
}