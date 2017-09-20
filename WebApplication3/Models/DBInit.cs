using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        protected override void Seed(DB context)
        {
            var kunde1 = new Customer
           {
                Id = 0,
                Firstname = "Andreas",
                Lastname = "Strand"
            };

            context.Customers.Add(kunde1);

            var airport1 = new Airport
            {
                Id = 0,
                Name = "Gardermoen"
            };

            var airport2 = new Airport
            {
                Id = 1,
                Name = "Flesland"
            };

            var airport3 = new Airport
            {
                Id = 2,
                Name = "Paris"
            };
        
            var airport4 = new Airport
            {
                Id = 3,
                Name = "London Gatwick"
            };

            context.Airports.Add(airport1);
            context.Airports.Add(airport2);
            context.Airports.Add(airport3);
            context.Airports.Add(airport4);

            var route1 = new Route
            {
                Id = 0,
                FromAirport = airport1,
                ToAirport = airport2
            };

            var route2 = new Route
            {
                Id = 1,
                FromAirport = airport1,
                ToAirport = airport3
            };

            var route3 = new Route
            {
                Id = 2,
                FromAirport = airport1,
                ToAirport = airport4
            };

            var route4 = new Route
            {
                Id = 4,
                FromAirport = airport2,
                ToAirport = airport3
            };
            context.Routes.Add(route1);
            context.Routes.Add(route2);
            context.Routes.Add(route3);

            var flight1 = new Flight
            {
                Id = 0,
                Time = new DateTime(2017,12,12),
                Route = route1
            };

            var flight2 = new Flight
            {
                Id = 1,
                Time = new DateTime(2017, 12, 13),
                Route = route1
            };

            var flight3 = new Flight
            {
                Id = 2,
                Time = new DateTime(2017, 12, 12),
                Route = route2
            };

            var flight4 = new Flight
            {
                Id = 3,
                Time = new DateTime(2017, 12, 13),
                Route = route4
            };

            context.Flights.Add(flight1);
            context.Flights.Add(flight2);
            context.Flights.Add(flight3);
            context.Flights.Add(flight4);

            base.Seed(context);
        }
    }
}