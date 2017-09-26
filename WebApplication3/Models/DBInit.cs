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
            var customer1 = new Customer
           {
                Id = 0,
                Firstname = "Andreas",
                Lastname = "Strand"
            };

            context.Customers.Add(customer1);

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

            var route5 = new Route
            {
                Id = 5,
                FromAirport = airport3,
                ToAirport = airport1
            };

            var route6 = new Route
            {
                Id = 6,
                FromAirport = airport3,
                ToAirport = airport2
            };

            var route7 = new Route
            {
                Id = 7,
                FromAirport = airport2,
                ToAirport = airport1
            };

            context.Routes.Add(route1);
            context.Routes.Add(route2);
            context.Routes.Add(route3);
            context.Routes.Add(route4);
            context.Routes.Add(route5);
            context.Routes.Add(route6);
            context.Routes.Add(route7);

            var flight1 = new Flight
            {
                Id = 0,
                Time = new DateTime(2017,09,12),
                Route = route1,
                Price = 99.99
            };

            var flight2 = new Flight
            {
                Id = 1,
                Time = new DateTime(2017, 09, 12),
                Route = route1,
                Price = 89.99
            };

            var flight3 = new Flight
            {
                Id = 2,
                Time = new DateTime(2017, 09, 12),
                Route = route2,
                Price = 40.0
            };

            var flight4 = new Flight
            {
                Id = 3,
                Time = new DateTime(2017, 09, 12),
                Route = route4,
                Price = 120.0
            };

            var flight5 = new Flight
            {
                Id = 4,
                Time = new DateTime(2017, 09, 13),
                Route = route5,
                Price = 55.0
            };

            var flight6 = new Flight
            {
                Id = 5,
                Time = new DateTime(2017, 09, 13),
                Route = route6,
                Price = 90.0
            };

            var flight7 = new Flight
            {
                Id = 6,
                Time = new DateTime(2017, 09, 13),
                Route = route7,
                Price = 60.0
            };

            context.Flights.Add(flight1);
            context.Flights.Add(flight2);
            context.Flights.Add(flight3);
            context.Flights.Add(flight4);
            context.Flights.Add(flight5);
            context.Flights.Add(flight6);
            context.Flights.Add(flight7);


            var order1 = new Order
            {
                Id = 0,
                Reference = "ABCD",
                Customer = customer1
            };

            context.Orders.Add(order1);

            var ticket1 = new Ticket
            {
                Id = 0,
                Order = order1,
                Flight = flight1
            };

            context.Tickets.Add(ticket1);

            base.Seed(context);
        }
    }
}