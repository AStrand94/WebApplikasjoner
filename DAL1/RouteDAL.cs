using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class RouteDAL
    {
        private DB db;

        public RouteDAL(DB db)
        {
            this.db = db;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            return db.Routes.ToList();
        }

        public bool RouteHasAirport(int id)
        {
            return db.Routes.Any(r => r.FromAirport.Id == id || r.ToAirport.Id == id);
        }
    }
}
