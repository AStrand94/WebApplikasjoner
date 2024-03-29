﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication3.DAL
{
    [ExcludeFromCodeCoverage]
    public class FlightDAL : IFlightDAL
    {
        public Flight GetFlight(int flightId)
        {
            using (DB db = new DB())
            {
                return db.Flights.Where(f => f.Id == flightId)
                    .Include("Tickets.Order").First();
            }

        }

        public IEnumerable<Flight> GetAllFlights()
        {
            using (DB db = new DB())
            {
                return db.Flights.ToList();
            }
        }

        public IEnumerable<Flight> GetAllFlightsWithFullRoute()
        {
            using(DB db = new DB())
            {
                return db.Flights
                    .Include("Route.ToAirport")
                    .Include("Route.FromAirport")
                    .ToList();
            }
        }

        public IEnumerable<Flight> GetAllFlightConnections()
        {
            using (DB db = new DB())
            {
                return db.Flights
                    .Include(f => f.Route)
                    .Include(f => f.Airplane)
                    .Include(f => f.Tickets)
                    .Include(f => f.Route.ToAirport)
                    .Include(f => f.Route.FromAirport)
                    .ToList();
            }
        }

        public Flight UpdateFlight(Flight flight)
        {
            using (DB db = new DB())
            {
                Flight dbFlight = db.Flights.Where(f => f.Id == flight.Id).Single();

                dbFlight.Route = db.Routes.Where(r => r.Id == flight.Route.Id).Single();
                dbFlight.Price = flight.Price;
                dbFlight.Time = flight.Time;
                dbFlight.Airplane = db.Airplanes.Attach(flight.Airplane);
                //DO NOT UPDATE TICKETS.

                db.SaveChanges();

                return dbFlight;
            }
        }

        public Flight DeleteFlight(int id)
        {
            using (DB db = new DB())
            {
                Flight flight = db.Flights.Where(f => f.Id == id).Single();

                if (flight != null)
                {
                    db.Flights.Attach(flight);
                    flight = db.Flights.Remove(flight);
                    db.SaveChanges();
                }

                return flight;
            }
        }

        public bool ExistsFlightWithId(int id)
        {
            using (DB db = new DB())
            {
                return db.Flights.Any(f => f.Id == id);
            }
        }

        public Flight InsertFlight(Flight flight)
        {
            using (DB db = new DB())
            {
                if (flight == null) return null;
                db.Routes.Attach(flight.Route);
                flight = db.Flights.Add(flight);
                db.SaveChanges();

                return flight;
            }
        }

        public Flight GetFlightWithInclude(int id)
        {
            using(DB db = new DB())
            {
                return db.Flights
                    .Where(f => f.Id == id)
                    .Include(f => f.Route)
                    .Include(f => f.Airplane)
                    .Include(f => f.Tickets)
                    .Include(f => f.Route.ToAirport)
                    .Include(f => f.Route.FromAirport)
                    .Single();
            }
        }
    }
}
