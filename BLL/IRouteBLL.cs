using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IRouteBLL
    {
        void AddRoute(Route route);
        string CanDeleteRoute(int id);
        Route DeleteRoute(int id);
        IEnumerable<Route> GetAllRoutes();
        bool RouteHasAirport(int id);
        void UpdateRoute(Route route);
    }
}