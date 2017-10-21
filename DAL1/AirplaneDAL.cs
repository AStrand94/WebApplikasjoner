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

        public Airplane InsertAirplane(Airplane airplane)
        {
            airplane = db.Airplanes.Add(airplane);
            db.SaveChanges();
            return airplane;
        }

        public Airplane GetAirplane(int id)
        {
            return db.Airplanes.Where(a => a.Id == id).Single();
        }

        public bool ExistsAirplaneWithId(int id)
        {
            return db.Airplanes.Any(a => a.Id == id);
        }

        public Airplane DeleteAirplane(int id)
        {
            Airplane airplane = db.Airplanes.Where(a => a.Id == id).Single();
            db.Airplanes.Attach(airplane);
            airplane = db.Airplanes.Remove(airplane);

            return airplane;
        }

        public bool Contains(int id)
        {
            return db.Airplanes.Any(a => a.Id == id);
        }

        public Airplane UpdateAirplane(Airplane airplane)
        {
            Airplane dbAirplane = db.Airplanes.Where(a => a.Id == airplane.Id).Single();
            dbAirplane.Model = airplane.Model;
            dbAirplane.Seats = airplane.Seats;
            db.SaveChanges();

            return dbAirplane;
        }
    }
}
