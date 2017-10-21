using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class FlightBLL
    {
        private DB db = new DB();
        private int fromAirportId;
        private int toAirportId;
        private DateTime date;
        private DateTime? returnDate;
        private int numberOfTravellers;

        public FlightBLL(int fromAirportId, int toAirportId, DateTime date, DateTime? returnDate, int numberOfTravellers)
        {
            this.fromAirportId = fromAirportId;
            this.toAirportId = toAirportId;
            this.date = date;
            this.returnDate = returnDate;
            this.numberOfTravellers = numberOfTravellers;
        }

        public FlightBLL()
        {

        }

        private List<Travel> GetFlightsTo()
        {
            PathHelper pathHelper = new PathHelper(fromAirportId, toAirportId, date, db);
            return pathHelper.GetAllFlights();
        }

        private List<Travel> GetFlightsFrom()
        {
            PathHelper pathHelper = new PathHelper(toAirportId, fromAirportId, returnDate.GetValueOrDefault(), db);
            List<Travel> ReturnFlights = pathHelper.GetAllFlights();
            ReturnFlights.ForEach(f => f.isReturnFlight = true);
            return ReturnFlights;
        }

        private List<Travel> GetFligthts()
        {
            List<Travel> Travels = GetFlightsTo();
            if (returnDate != null)
            {
                List<Travel> RetTravels = GetFlightsFrom();
                RetTravels.ForEach(t => t.isReturnFlight = true);
                Travels.AddRange(RetTravels);
            }

            return Travels;
        }

        public TravelModel GetTravelModel()
        {
            AirportDAL airportDAL = new AirportDAL(db);
            if(returnDate == null)
            {
                return new TravelModel(GetFlightsTo(), airportDAL.GetById(fromAirportId), airportDAL.GetById(toAirportId));
            }
            else
            {
                return new TravelModel(GetFlightsTo(), GetFlightsFrom(), airportDAL.GetById(fromAirportId), airportDAL.GetById(toAirportId));
            }
        }

        public List<Flight> GetFlights(List<int> flightIds)
        {
            List<Flight> flights = new List<Flight>();
            FlightDAL flightDAL = new FlightDAL(db);

            foreach (int id in flightIds)
            {
                flights.Add(flightDAL.GetFlight(id));
            }

            return flights;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return new FlightDAL(db).GetAllFlights();
        }

        public String CanUpdateFlight(Flight flight)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (flight.Price == 0 || flight.Route == null || flight.Time == null || flight.Id < 0)
            {
                stringBuilder.Append("All field must be filled!\n\r");
            }

            if (flight.Time != null && flight.Time < DateTime.Now)
            {
                stringBuilder.Append("Time cannot be before now!\n\r");
            }

            return stringBuilder.ToString();
        }

        public void UpdateFlight(Flight flight)
        {
            FlightDAL flightDAL = new FlightDAL(db);
            RouteDAL routeDAL = new RouteDAL(db);
            AirplaneDAL airplaneDAL = new AirplaneDAL(db);

            Flight dbFlight = flightDAL.GetFlight(flight.Id);

            if (dbFlight == null)
                throw new NullReferenceException("flight with id " + flight.Id + " does not exist in current context");
            
            flight.Route = routeDAL.GetRoute(flight.Route.Id);
            flight.Airplane = airplaneDAL.GetAirplane(flight.Airplane.Id);
            flightDAL.UpdateFlight(flight);
        }


    }
}
