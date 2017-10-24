using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class RouteBLL : IRouteBLL
    {
        private IRouteDAL route;

        public RouteBLL()
        {
            route = new RouteDAL();
        }

        public RouteBLL(IRouteDAL stub)
        {
            route = stub;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            return new RouteDAL().GetAllRoutes();
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            return new RouteDAL().GetAllRoutesConnections();
        }

        public bool RouteHasAirport(int id)
        {
            return new RouteDAL().RouteHasAirport(id);
        }

        public void UpdateRoute(Route route)
        {
            AirportDAL airportDAL = new AirportDAL();
            RouteDAL routeDAL = new RouteDAL();

            Route dbRoute = routeDAL.GetRoute(route.Id);

            if (dbRoute == null)
                throw new NullReferenceException("Route with id " + route.Id + " does not exist in current context");

            route.FromAirport = airportDAL.GetAirport(route.FromAirport.Id);
            route.ToAirport = airportDAL.GetAirport(route.ToAirport.Id);
            routeDAL.UpdateRoute(route);
        }

        public Route DeleteRoute(int id)
        {
            return new RouteDAL().DeleteRoute(id);
        }

        public string CanDeleteRoute(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Route route = new RouteDAL().GetRoute(id);

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
            AirportDAL airportDAL = new AirportDAL();
            RouteDAL routeDAL = new RouteDAL();

            route.FromAirport = airportDAL.GetAirport(route.FromAirport.Id);
            route.ToAirport = airportDAL.GetAirport(route.ToAirport.Id);

            routeDAL.AddRoute(route);
        }
    }
}
