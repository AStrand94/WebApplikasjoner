using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class RouteBLL
    {
        private DB db = new DB();

        public IEnumerable<Route> GetAllRoutes()
        {
            return new RouteDAL(db).GetAllRoutes();
        }

        public bool RouteHasAirport(int id)
        {
            return new RouteDAL(db).RouteHasAirport(id);
        }

        public void UpdateRoute(Route route)
        {
            AirportDAL airportDAL = new AirportDAL(db);
            RouteDAL routeDAL = new RouteDAL(db);

            Route dbRoute = routeDAL.GetRoute(route.Id);

            if (dbRoute == null)
                throw new NullReferenceException("Route with id " + route.Id + " does not exist in current context");

            route.FromAirport = airportDAL.GetAirport(route.FromAirport.Id);
            route.ToAirport = airportDAL.GetAirport(route.ToAirport.Id);
            routeDAL.UpdateRoute(route);
        }

        public Route DeleteRoute(int id)
        {
            return new RouteDAL(db).DeleteRoute(id);
        }

        public string CanDeleteRoute(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Route route = new RouteDAL(db).GetRoute(id);

            if (route == null)
            {
                stringBuilder.Append("Route does not exist");
            }
            else
            {
                if (route.Flights.Count > 0)
                {
                    HashSet<string> routes = new HashSet<string>();
                    foreach (var flight in route.Flights)
                    {
                        routes.Add(flight.Time.ToShortDateString());
                    }

                    stringBuilder.Append("Cannot delete route, must delete flights from ").
                        Append(route.FromAirport.Name).Append(" to ").Append(route.ToAirport.Name).Append(" on dates: ");

                    foreach (var str in routes)
                    {
                        stringBuilder.Append(str + ',');
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }

            return stringBuilder.ToString();
        }

        public void AddRoute(Route route)
        {
            AirportDAL airportDAL = new AirportDAL(db);
            RouteDAL routeDAL = new RouteDAL(db);

            route.FromAirport = airportDAL.GetAirport(route.FromAirport.Id);
            route.ToAirport = airportDAL.GetAirport(route.ToAirport.Id);

            routeDAL.AddRoute(route);
        }
    }
}
