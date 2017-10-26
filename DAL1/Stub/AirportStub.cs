using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class AirportStub : IAirportDAL
    {
        public Airport AddAirport(Airport airport)
        {
            if (airport.Name == "")
            {
                var ap = new Airport
                {
                    Name = ""
                };
                return ap;
            }
            else
            {
                var ap = new Airport
                {
                    Name = "Gardermoen"
                };
                return ap;
            }
        }

        public Airport DeleteAirport(int id)
        {
            if (id == 0)
            {
                var airport = new Airport
                {
                    Id = 0
                };
                return airport;
            }
            else
            {
                return GetAirport(1);
            }
        }

        public Airport GetAirport(int id)
        {
            if (id == 0)
            {
                var airport = new Airport
                {
                    Id = 0
                };
                return airport;
            }
            else
            {
                var airport = new Airport()
                {
                    Id = 1,
                    Name = "Gardermoen",
                    Code = "OSL",
                    Country = "Norway",
                    City = "Oslo"
                };
                return airport;
            }
        }

        public List<Airport> GetAllAirports()
        {
            var airportList = new List<Airport>();
            var airport = GetAirport(1);

            airportList.Add(airport);
            airportList.Add(airport);
            airportList.Add(airport);

            return airportList;
        }

        public Airport UpdateAirport(Airport airport)
        {
            if (airport.Id == 0)
            {
                var a = new Airport
                {
                    Id = 0
                };
                return a;
            }
            else
            {
                return GetAirport(1);
            }
        }
    }
}
