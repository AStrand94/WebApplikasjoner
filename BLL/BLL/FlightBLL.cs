using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class FlightBLL : IFlightBLL
    {
        private int fromAirportId;
        private int toAirportId;
        private DateTime date;
        private DateTime? returnDate;
        private int numberOfTravellers;

        private IFlightDAL _flightDAL;

        public FlightBLL(int fromAirportId, int toAirportId, DateTime date, DateTime? returnDate, int numberOfTravellers)
        {
            this.fromAirportId = fromAirportId;
            this.toAirportId = toAirportId;
            this.date = date;
            this.returnDate = returnDate;
            this.numberOfTravellers = numberOfTravellers;
            _flightDAL = new FlightDAL();
        }

        public FlightBLL()
        {
            _flightDAL = new FlightDAL();
        }
        
        public FlightBLL(IFlightDAL stub)
        {
            _flightDAL = stub;
        }

        private List<Travel> GetFlightsTo()
        {
            PathHelper pathHelper = new PathHelper(fromAirportId, toAirportId, date, new DB());
            return pathHelper.GetAllFlights();
        }

        private List<Travel> GetFlightsFrom()
        {
            PathHelper pathHelper = new PathHelper(toAirportId, fromAirportId, returnDate.GetValueOrDefault(), new DB());
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
            AirportDAL airportDAL = new AirportDAL();
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
            FlightDAL flightDAL = new FlightDAL();

            foreach (int id in flightIds)
            {
                flights.Add(flightDAL.GetFlightWithInclude(id));
            }

            return flights;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _flightDAL.GetAllFlights();
        }

        public IEnumerable<Flight> GetAllFlightsWithFullRoute()
        {
            return flightDAL.GetAllFlightsWithFullRoute();
        }

        public IEnumerable<Flight> GetAllFlightConnections()
        {
            return _flightDAL.GetAllFlightConnections();
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
            RouteDAL routeDAL = new RouteDAL();
            AirplaneDAL airplaneDAL = new AirplaneDAL();

            Flight dbFlight = _flightDAL.GetFlight(flight.Id);

            if (dbFlight == null)
                throw new NullReferenceException("flight with id " + flight.Id + " does not exist in current context");
            
            flight.Route = routeDAL.GetRoute(flight.Route.Id);
            flight.Airplane = airplaneDAL.GetAirplane(flight.Airplane.Id);
            _flightDAL.UpdateFlight(flight);
        }

        public string CanDeleteFlight(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Flight flight = _flightDAL.GetFlight(id);

            if(flight == null)
            {
                stringBuilder.Append("Flight does not exist");
            }
            else
            {
                if(flight.Tickets.Count > 0)
                {
                    HashSet<string> orders = new HashSet<string>();
                    foreach(var ticket in flight.Tickets)
                    {
                        orders.Add(ticket.Order.Reference);
                    }

                    stringBuilder.Append("Cannot delete flight, must delete orders: ");

                    foreach(var str in orders)
                    {
                        stringBuilder.Append(str + ',');
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1,1);
                }
            }

            return stringBuilder.ToString();
        }

       
        public Flight DeleteFlight(int id)
        {
            return new FlightDAL().DeleteFlight(id);
        }

        public string CanInsertFlight(Flight flight)
        {
            StringBuilder stringBuilder = new StringBuilder();
            RouteDAL routeDAL = new RouteDAL();
            AirplaneDAL airplaneDAL = new AirplaneDAL();

            if(flight.Price == 0)
            {
                stringBuilder.Append("Price cannot be 0!\n\r");
            }

            if (!routeDAL.ExistsRouteWithId(flight.Route.Id))
            {
                stringBuilder.Append("Route does not exist");
            }

            if (!airplaneDAL.ExistsAirplaneWithId(flight.Airplane.Id))
            {
                stringBuilder.Append("Airplane does not exist");
            }

            if(flight.Time == null || flight.Time < DateTime.Now)
            {
                stringBuilder.Append("Flight cannot be before now!");
            }

            return stringBuilder.ToString();
        }

        public Flight InsertFlight(Flight flight)
        {
            flight.Route = new RouteDAL().GetRoute(flight.Route.Id);
            flight.Airplane = new AirplaneDAL().GetAirplane(flight.Airplane.Id);
            return _flightDAL.InsertFlight(flight);
        }
    }
}
