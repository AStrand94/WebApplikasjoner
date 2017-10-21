using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class AirportDAL
    {
        private DB db;

        public AirportDAL(DB db)
        {
            this.db = db;
        }

        public List<Airport> GetAllAirports() {
            return db.Airports.ToList();
        }

        public Airport GetById(int Id)
        {
            return db.Airports.Where(a => a.Id == Id).Single();
        }

        public void AddAirport(Airport airport)
        {
            db.Airports.Add(airport);
            db.SaveChanges();
        }

        public Airport GetAirport(int id)
        {
            return db.Airports.Where(a => a.Id == id).Single();
        }

        public void UpdateAirport(Airport airport)
        {
            Airport airportInDb = db.Airports.Single(a => a.Id == airport.Id);
            airportInDb.Name = airport.Name;
            airportInDb.Code = airport.Code;
            airportInDb.Country = airport.Country;
            airportInDb.City = airport.City;

            db.SaveChanges();
        }

        public Airport DeleteAirport(int id)
        {
            //Må slette avhengigheter først. Ikke slett for kunder som har ordre.
            Airport airport = db.Airports.Where(a => a.Id == id).Single();

            if (airport != null)
            {
                db.Airports.Attach(airport);
                airport = db.Airports.Remove(airport);
                db.SaveChanges();
            }

            return airport;
        }
    }
}
