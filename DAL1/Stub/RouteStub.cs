using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class RouteStub : IRouteDAL
    {
        public void AddRoute(Route route)
        {
        }

        public Route DeleteRoute(int id)
        {
            Airport airport = new Airport
            {
                Id = 0,
                Code = "OSL",
                City = "Oslo",
                Country = "Norway",
                Name = "Gardermoen"
            };

            return new Route
            {
                ToAirport = airport,
                FromAirport = airport,
                FlightTime = new TimeSpan(10,0,0),
                Flights = new List<Flight>()
            };
        }

        public bool ExistsRouteWithId(int id)
        {
            return true;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            Airport airport1 = new Airport
            {
                Id = 0,
                Code = "OSL",
                City = "Oslo",
                Country = "Norway",
                Name = "Gardermoen"
            };

            Airport airport2 = new Airport
            {
                Id = 0,
                Code = "lll",
                City = "Paris",
                Country = "France",
                Name = "Paris airport"
            };

            Route r1 = new Route
            {
                ToAirport = airport1,
                FromAirport = airport2,
                FlightTime = new TimeSpan(10, 0, 0),
                Flights = new List<Flight>()
            };

            Route r2 = new Route
            {
                ToAirport = airport2,
                FromAirport = airport1,
                FlightTime = new TimeSpan(12, 0, 0),
                Flights = new List<Flight>()
            };

            List<Route> routes = new List<Route>();
            routes.Add(r1); routes.Add(r2);

            return routes;
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            return GetAllRoutes();
        }

        public Route GetRoute(int id)
        {
            return GetAllRoutes().ElementAt(0);
        }

        public bool RouteHasAirport(int id)
        {
            return false;
        }

        public void UpdateRoute(Route route)
        {
        }
    }
}
