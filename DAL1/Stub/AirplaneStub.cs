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
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Airplane DeleteAirplane(int id)
        {
            if (id == 0)
            {
                var airplane = new Airplane();
                airplane.Id = 0;
                return airplane;
            } else
            {
                return GetAirplane(1);
            }
        }

        public bool ExistsAirplaneWithId(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Airplane GetAirplane(int id)
        {
            if(id == 0)
            {
                var airplane = new Airplane();
                airplane.Id = 0;
                return airplane;
            } else
            {
                var airplane = new Airplane()
                {
                    Id = 1,
                    Model = "Boeing 737",
                    Seats = 148
                };
                return airplane;
            }
        }

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            var airplaneList = new List<Airplane>();
            var airplane = GetAirplane(1);

            airplaneList.Add(airplane);
            airplaneList.Add(airplane);
            airplaneList.Add(airplane);

            return airplaneList;
        }

        public bool HasFlights(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Airplane InsertAirplane(Airplane airplane)
        {
            if(airplane.Seats == 0)
            {
                var ap = new Airplane();
                ap.Seats = 0;
                return airplane;
            } else
            {
                return airplane;
            }
        }

        public Airplane UpdateAirplane(Airplane airplane)
        {
            if(airplane.Id == 0)
            {
                var ap = new Airplane();
                ap.Id = 0;
                return ap;
            } else
            {
                return GetAirplane(1);
            }
        }
    }
}
