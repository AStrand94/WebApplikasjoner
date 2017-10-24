using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class AirplaneStub : IAirplaneDAL
    {
        public bool Contains(int id)
        {
            throw new NotImplementedException();
        }

        public Airplane DeleteAirplane(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsAirplaneWithId(int id)
        {
            throw new NotImplementedException();
        }

        public Airplane GetAirplane(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            var airplaneList = new List<Airplane>();
            var airplane = new Airplane()
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            airplaneList.Add(airplane);
            airplaneList.Add(airplane);
            airplaneList.Add(airplane);

            return airplaneList;
        }

        public bool HasFlights(int id)
        {
            throw new NotImplementedException();
        }

        public Airplane InsertAirplane(Airplane airplane)
        {
            throw new NotImplementedException();
        }

        public Airplane UpdateAirplane(Airplane airplane)
        {
            throw new NotImplementedException();
        }
    }
}
