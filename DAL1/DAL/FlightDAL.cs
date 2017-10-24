﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using System.Data.Entity;

namespace WebApplication3.DAL
{
    public class FlightDAL : IFlightDAL
    {
        public Flight GetFlight(int flightId)
        {
            using (DB db = new DB())
            {
                return db.Flights.Where(f => f.Id == flightId).First();
            }

        }

        public IEnumerable<Flight> GetAllFlights()
        {
            using (DB db = new DB())
            {
                return db.Flights.ToList();
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

        public void UpdateFlight(Flight flight)
        {
            using (DB db = new DB())
            {
                Flight dbFlight = GetFlight(flight.Id);
                dbFlight.Route = flight.Route;
                dbFlight.Price = flight.Price;
                dbFlight.Time = flight.Time;
                dbFlight.Airplane = flight.Airplane;
                //DO NOT UPDATE TICKETS.

                db.SaveChanges();
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

                flight = db.Flights.Add(flight);
                db.SaveChanges();

                return flight;
            }
        }
    }
}