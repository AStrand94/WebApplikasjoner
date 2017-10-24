using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class AirportBLL : IAirportBLL
    {
        private IAirportDAL airport;

        public AirportBLL()
        {
            airport = new AirportDAL();
        }

        public AirportBLL(IAirportDAL stub)
        {
            airport = stub;
        }

        public List<Airport> GetAllAirports()
        {
            return new AirportDAL().GetAllAirports();
        }

        public Airport GetById(int Id)
        {
            return new AirportDAL().GetById(Id);
        }

        public void AddAirport(Airport airport)
        {
            new AirportDAL().AddAirport(airport);
        }

        public void UpdateAirport(Airport airport)
        {
            new AirportDAL().UpdateAirport(airport);
        }

        public bool AirportIsUsedInRoutes(int id)
        {
            Airport airport = new AirportDAL().GetAirport(id);
            bool routeHasAirport = new RouteBLL().RouteHasAirport(id);

            return airport != null && !routeHasAirport;
        }

        public Airport DeleteAirport(int id)
        {
            return new AirportDAL().DeleteAirport(id);
        }
    }
}
