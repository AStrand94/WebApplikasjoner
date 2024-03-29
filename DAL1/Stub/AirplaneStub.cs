﻿using System;
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
            if (id == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Airplane InsertAirplane(Airplane airplane)
        {
            if(airplane.Seats == 0)
            {
                var ap = new Airplane
                {
                    Seats = 0
                };
                return airplane;
            } else
            {
                return airplane;
            }
        }

        public Airplane UpdateAirplane(Airplane airplane)
        {
            return GetAirplane(1);
        }
    }
}
