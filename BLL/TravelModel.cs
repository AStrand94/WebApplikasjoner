using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using WebApplication3.Model;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class TravelModel
    {
        public List<Travel> Travels { get; set; }
        public List<Travel> ReturnTravels { get; set; }
        public bool IsReturn { get; set; }
        public Airport FromAirport { get; set; }
        public Airport ToAirport { get; set; }

        public TravelModel(List<Travel> Travels,Airport fromAirport, Airport toAirport)
        {
            this.Travels = Travels;
            this.FromAirport = fromAirport;
            this.ToAirport = toAirport;
            IsReturn = false;
            this.Travels.ForEach(f => f.Init());
        }

        public TravelModel(List<Travel> Travels, List<Travel> ReturnTravels, Airport fromAirport, Airport toAirport)
        {
            this.Travels = Travels;
            this.ReturnTravels = ReturnTravels;
            IsReturn = true;
            this.FromAirport = fromAirport;
            this.ToAirport = toAirport;
            this.Travels.ForEach(f => f.Init());
            this.ReturnTravels.ForEach(f => f.Init());
        }

        public bool IsValidRoute()
        {
            return !(IsReturn == true && !ReturnTravels.Any()) || !Travels.Any();
        }
    }
}