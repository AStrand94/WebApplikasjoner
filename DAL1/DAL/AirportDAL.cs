using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class AirportDAL : IAirportDAL
    {
        public List<Airport> GetAllAirports()
        {
            using (DB db = new DB())
            {
                return db.Airports.ToList();
            }
        }

        public Airport GetById(int Id)
        {
            using (DB db = new DB())
            {
                return db.Airports.Where(a => a.Id == Id).Single();
            }
        }

        public Airport AddAirport(Airport airport)
        {
            using (DB db = new DB())
            {
                airport = db.Airports.Add(airport);
                db.SaveChanges();
                return airport;
            }
        }

        public Airport GetAirport(int id)
        {
            using (DB db = new DB())
            {
                return db.Airports.Where(a => a.Id == id).Single();
            }
        }

        public Airport UpdateAirport(Airport airport)
        {
            using (DB db = new DB())
            {
                Airport airportInDb = db.Airports.Single(a => a.Id == airport.Id);
                airportInDb.Name = airport.Name;
                airportInDb.Code = airport.Code;
                airportInDb.Country = airport.Country;
                airportInDb.City = airport.City;

                db.SaveChanges();

                return airportInDb;
            }
        }

        public Airport DeleteAirport(int id)
        {
            using (DB db = new DB())
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
}
