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

        public List<Airport> getAllAirports() {
            return db.Airports.ToList();
        }

        public Airport GetById(int Id)
        {
            return db.Airports.Where(a => a.Id == Id).Single();
        }
    }
}
