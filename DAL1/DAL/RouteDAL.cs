using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using System.Data.Entity;

namespace WebApplication3.DAL
{
    public class RouteDAL : IRouteDAL
    {
        public IEnumerable<Route> GetAllRoutes()
        {
            using (DB db = new DB())
            {
                return db.Routes
                    .Include(r => r.FromAirport)
                    .Include(r => r.ToAirport)
                    .ToList();
            }
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            using (DB db = new DB())
            {
                return db.Routes
                    .Include(r => r.FromAirport)
                    .Include(r => r.ToAirport)
                    .ToList();
            }
        }

        public bool RouteHasAirport(int id)
        {
            using (DB db = new DB())
            {

                return db.Routes.Any(r => r.FromAirport.Id == id || r.ToAirport.Id == id);
            }
        }

        public Route GetRoute(int id)
        {
            using (DB db = new DB())
            {

                return db.Routes.Where(r => r.Id == id).Single();
            }
        }

        public void UpdateRoute(Route route)
        {
            using (DB db = new DB())
            {
                Route routeInDb = GetRoute(route.Id);
                routeInDb.FromAirport = route.FromAirport;
                routeInDb.ToAirport = route.ToAirport;
                routeInDb.FlightTime = route.FlightTime;

                db.SaveChanges();
            }
        }

        public bool ExistsRouteWithId(int id)
        {
            using (DB db = new DB())
            {
                return db.Routes.Any(r => r.Id == id);
            }
        }

        public Route DeleteRoute(int id)
        {
            using (DB db = new DB())
            {
                Route route = GetRoute(id);

                if (route != null)
                {
                    db.Routes.Attach(route);
                    route = db.Routes.Remove(route);
                    db.SaveChanges();
                }

                return route;
            }
        }

        public void AddRoute(Route route)
        {
            using (DB db = new DB())
            {
                db.Routes.Add(route);
                db.SaveChanges();
            }
        }
    }
}
