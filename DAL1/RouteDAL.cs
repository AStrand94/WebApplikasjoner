using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class RouteDAL : IRouteDAL
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

        public Route GetRoute(int id)
        {
            return db.Routes.Where(r => r.Id == id).Single();
        }

        public void UpdateRoute(Route route)
        {
            Route routeInDb = GetRoute(route.Id);
            routeInDb.FromAirport = route.FromAirport;
            routeInDb.ToAirport = route.ToAirport;
            routeInDb.FlightTime = route.FlightTime;

            db.SaveChanges();
        }

        public bool ExistsRouteWithId(int id)
        {
            return db.Routes.Any(r => r.Id == id);
        }

        public Route DeleteRoute(int id)
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

        public void AddRoute(Route route)
        {
            db.Routes.Add(route);
            db.SaveChanges();
        }
    }
}
