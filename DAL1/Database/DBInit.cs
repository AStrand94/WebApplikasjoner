using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        private byte[] GetCryptedPassword(String password)
        {
            var algorithm = System.Security.Cryptography.SHA512.Create();

            byte[] inData = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] outData = algorithm.ComputeHash(inData);

            return outData;
        }

        protected override void Seed(DB context)
        {

            var adminUser = new User
            {
                Id = 0,
                Username = "admin",
                Password = GetCryptedPassword("admin")
            };

            context.Users.Add(adminUser);

            var customer1 = new Customer
            {
                Id = 0,
                Firstname = "Andreas",
                Lastname = "Strand",
                Telephone = "90834049",
                Email = "strand@hotmail.com"
            };

            var customer2 = new Customer
            {
                Id = 1,
                Firstname = "Stian",
                Lastname = "Grimsgaard",
                Telephone = "12345678",
                Email = "stian@gmail.com"
            };

            context.Customers.Add(customer1);
            context.Customers.Add(customer2);


            var airplane1 = new Airplane
            {
                Id = 0,
                Model = "Boeing 777",
                Seats = 120
            };

            var airplane2 = new Airplane
            {
                Id = 1,
                Model = "Airbus A380",
                Seats = 110
            };

            context.Airplanes.Add(airplane1);
            context.Airplanes.Add(airplane2);
            
            var airport1 = new Airport
            {
                Id = 0,
                Name = "Gardermoen",
                City = "Oslo",
                Country = "Norway",
                Code = "OSL"
            };

            var airport2 = new Airport
            {
                Id = 1,
                Name = "Flesland",
                City = "Bergen",
                Country = "Norway",
                Code = "BGO"
            };

            var airport3 = new Airport
            {
                Id = 2,
                Name = "Charles de Gaulle",
                City = "Paris",
                Country = "France",
                Code = "CDG"
            };
        
            var airport4 = new Airport
            {
                Id = 3,
                Name = "London Gatwick",
                City = "London",
                Country = "England",
                Code = "LGW"
            };

            context.Airports.Add(airport1);
            context.Airports.Add(airport2);
            context.Airports.Add(airport3);
            context.Airports.Add(airport4);

            var route1 = new Route
            {
                Id = 0,
                FromAirport = airport1,
                ToAirport = airport2,
                FlightTime = new TimeSpan(0,45,0)
            };

            var route2 = new Route
            {
                Id = 1,
                FromAirport = airport1,
                ToAirport = airport3,
                FlightTime = new TimeSpan(2,0,0)
            };

            var route3 = new Route
            {
                Id = 2,
                FromAirport = airport1,
                ToAirport = airport4,
                FlightTime = new TimeSpan(1,30,0)
            };

            var route4 = new Route
            {
                Id = 4,
                FromAirport = airport2,
                ToAirport = airport3,
                FlightTime = new TimeSpan(1,0,0)
            };

            var route5 = new Route
            {
                Id = 5,
                FromAirport = airport3,
                ToAirport = airport1,
                FlightTime = new TimeSpan(1,45,0)
            };

            var route6 = new Route
            {
                Id = 6,
                FromAirport = airport3,
                ToAirport = airport2,
                FlightTime = new TimeSpan(2,15,0)
            };

            var route7 = new Route
            {
                Id = 7,
                FromAirport = airport2,
                ToAirport = airport1,
                FlightTime = new TimeSpan(0,40,0)
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
                Time = new DateTime(2017,09,12,12,00,00),
                Route = route1,
                Price = 99.99,
                Airplane = airplane1
            };

            var flight2 = new Flight
            {
                Id = 1,
                Time = new DateTime(2017, 09, 12,13,25,00),
                Route = route1,
                Price = 89.99,
                Airplane = airplane2
            };

            var flight3 = new Flight
            {
                Id = 2,
                Time = new DateTime(2017, 09, 12,17,30,00),
                Route = route2,
                Price = 40.0,
                Airplane = airplane2
            };

            var flight4 = new Flight
            {
                Id = 3,
                Time = new DateTime(2017, 09, 12,14,00,00),      //reise fra Flesland til Paris
                Route = route4,
                Price = 120.0,
                Airplane = airplane1
            };

            var flight5 = new Flight
            {
                Id = 4,
                Time = new DateTime(2017, 09, 13,20,30,00),
                Route = route5,
                Price = 55.0,
                Airplane = airplane2
            };

            var flight6 = new Flight
            {
                Id = 5,
                Time = new DateTime(2017, 09, 13,07,50,00),
                Route = route6,
                Price = 90.0,
                Airplane = airplane1
            };

            var flight7 = new Flight
            {
                Id = 6,
                Time = new DateTime(2017, 09, 13,11,00,00),
                Route = route7,
                Price = 60.0,
                Airplane = airplane2
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
                Flight = flight1,
                FirstName = "Andreas",
                LastName = "Strand"
            };

            var ticket2 = new Ticket
            {
                Id = 1,
                Order = order1,
                Flight = flight2,
                FirstName = "Stian",
                LastName = "Grimsgaard"
            };

            var ticket3 = new Ticket
            {
                Id = 2,
                Order = order1,
                Flight = flight3,
                FirstName = "Nils",
                LastName = "Petter"
            };

            var ticket4 = new Ticket
            {
                Id = 3,
                Order = order1,
                Flight = flight4,
                FirstName = "Fridtjof",
                LastName = "Hansen"
            };

            context.Tickets.Add(ticket1);
            context.Tickets.Add(ticket2);
            context.Tickets.Add(ticket3);
            context.Tickets.Add(ticket4);

            base.Seed(context);
        }
    }
}