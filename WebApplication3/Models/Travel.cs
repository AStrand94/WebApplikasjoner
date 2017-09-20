using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Travel
    {
        public Travel(Airport From, Airport To)
        {
            ToAirport = To;
            FromAirport = From;
            TravelList = new List<Flight>();
        }

        public Travel(Flight FromFlight, Flight ToFlight)
        {
            this.ToFlight = ToFlight;
            this.FromFlight = FromFlight;
            isSingleFlight = false;
        }

        public Travel(Flight flight)
        {
            FromFlight = flight;
            isSingleFlight = true;
        }

        List<Flight> TravelList;
        Airport FromAirport, ToAirport;
        public Flight FromFlight { get; }
        public Flight ToFlight { get; }
        public bool isSingleFlight { get; set; }

        public void addFlight(Flight f)
        {
            TravelList.Add(f);
        }

        public List<Flight> getAllFlights()
        {
            return TravelList;
        }


            
    }
}