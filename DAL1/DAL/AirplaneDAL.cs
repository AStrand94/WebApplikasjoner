using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    [ExcludeFromCodeCoverage]
    public class AirplaneDAL : IAirplaneDAL
    {

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            using (DB db = new DB())
            {
                return db.Airplanes.ToList();
            }
        }

        public Airplane InsertAirplane(Airplane airplane)
        {
            using (DB db = new DB())
            {
                airplane = db.Airplanes.Add(airplane);
                db.SaveChanges();
                return airplane;
            }
        }

        public Airplane GetAirplane(int id)
        {
            using (DB db = new DB())
            {
                return db.Airplanes.Where(a => a.Id == id).Single();
            }
        }

        public bool ExistsAirplaneWithId(int id)
        {
            using (DB db = new DB())
            {
                return db.Airplanes.Any(a => a.Id == id);
            }
        }

        public bool HasFlights(int id)
        {
            using(DB db = new DB())
            {
                Airplane airplane = db.Airplanes.Where(a => a.Id == id).Single();
                return airplane.Flights.Any();
            }
        }

        public Airplane DeleteAirplane(int id)
        {
            using (DB db = new DB())
            {
                Airplane airplane = db.Airplanes.Where(a => a.Id == id).Single();
                db.Airplanes.Attach(airplane);
                airplane = db.Airplanes.Remove(airplane);
                db.SaveChanges();

                return airplane;
            }
        }

        public bool Contains(int id)
        {
            using (DB db = new DB())
            {
                return db.Airplanes.Any(a => a.Id == id);
            }
        }

        public Airplane UpdateAirplane(Airplane airplane)
        {
            using (DB db = new DB())
            {
                Airplane dbAirplane = db.Airplanes.Where(a => a.Id == airplane.Id).Single();
                dbAirplane.Model = airplane.Model;
                dbAirplane.Seats = airplane.Seats;
                db.SaveChanges();

                return dbAirplane;
            }
        }
    }
}
