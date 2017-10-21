using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class AirportBLL
    {
        private DB db = new DB();
        
        public List<Airport> GetAllAirports()
        {
            return new AirportDAL(db).GetAllAirports();
        }

        public Airport GetById(int Id)
        {
            return new AirportDAL(db).GetById(Id);
        }

        public void AddAirport(Airport airport)
        {
            new AirportDAL(db).AddAirport(airport);
        }

        public void UpdateAirport(Airport airport)
        {
            new AirplaneDAL(db).UpdateAirport(airport);
        }

        public bool AirportIsUsedInRoutes(int id)
        {
            Airport airport = new AirportDAL(db).GetAirport(id);
            bool routeHasAirport = new RouteBLL().RouteHasAirport(id);

            return airport != null && !routeHasAirport;
        }

        public Airport DeleteAirport(int id)
        {
            return new AirportDAL(db).DeleteAirport(id);
        }
    }
}
