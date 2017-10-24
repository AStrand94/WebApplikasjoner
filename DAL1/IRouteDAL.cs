using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IRouteDAL
    {
        void AddRoute(Route route);
        Route DeleteRoute(int id);
        bool ExistsRouteWithId(int id);
        IEnumerable<Route> GetAllRoutes();
        Route GetRoute(int id);
        bool RouteHasAirport(int id);
        void UpdateRoute(Route route);
    }
}