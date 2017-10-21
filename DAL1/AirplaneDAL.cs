using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class AirplaneDAL
    {
        private DB db;

        public AirplaneDAL(DB db)
        {
            this.db = db;
        }

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            return db.Airplanes.ToList();
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

        public Airplane GetAirplane(int id)
        {
            return db.Airplanes.Where(a => a.Id == id).Single();
        }
    }
}
