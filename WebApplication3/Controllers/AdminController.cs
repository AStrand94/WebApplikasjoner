using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.BLL;
using WebApplication3.Model;
using DTO;
using WebApplication3.Logging;
using System.Text;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
        private IAirplaneBLL _airplaneBLL;
        private IAirportBLL _airportBLL;
        private ICustomerBLL _customerBLL;
        private IFlightBLL _flightBLL;
        private ILoginBLL _loginBLL;
        private IOrderBLL _orderBLL;
        private IRouteBLL _routeBLL;
        private ITicketBLL _ticketBLL;

        public AdminController()
        {
            _airplaneBLL = new AirplaneBLL();
            _airportBLL = new AirportBLL();
            _customerBLL = new CustomerBLL();
            _flightBLL = new FlightBLL();
            _loginBLL = new LoginBLL();
            _orderBLL = new OrderBLL();
            _routeBLL = new RouteBLL();
            _ticketBLL = new TicketBLL();
        }

        //If unit testing
        public AdminController(IAirplaneBLL airplaneStub, IAirportBLL airportStub, ICustomerBLL customerStub, IFlightBLL flightStub, ILoginBLL loginStub, IOrderBLL orderStub, IRouteBLL routeStub, ITicketBLL ticketStub)
        {
            _airplaneBLL = airplaneStub;
            _airportBLL = airportStub;
            _customerBLL = customerStub;
            _flightBLL = flightStub;
            _loginBLL = loginStub;
            _orderBLL = orderStub;
            _routeBLL = routeStub;
            _ticketBLL = ticketStub;
        }

        public ActionResult Login(string username, string password)
        {
            if (_loginBLL.checkLogin(username, password))
            {
                Session["LoggedIn"] = true;
                Session["LoggedInUser"] = username;

                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult LogOut()
        {
            Session["loggedIn"] = false;
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Index()
        {
            if(!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            ViewBag.loggedInUser = Session["loggedInUser"];
            ViewBag.Customers = _customerBLL.GetAllCustomers().Count();
            ViewBag.Orders = _orderBLL.GetAllOrders().Count();
            ViewBag.Flights = _flightBLL.GetAllFlights().Count();
            ViewBag.Routes = _routeBLL.GetAllRoutes().Count();
            ViewBag.Airplanes = _airplaneBLL.GetAllAirplanes().Count();
            ViewBag.Airports = _airportBLL.GetAllAirports().Count();

            return View();
        }

        public ActionResult Airplanes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(_airplaneBLL.GetAllAirplanes().OrderBy(a => a.Model).ToList());
        }

        public ActionResult Airports()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(_airportBLL.GetAllAirports().OrderBy(a => a.Name).ToList());

        }

        public ActionResult Customers()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(_customerBLL.GetAllCustomersConnections().OrderBy(c => c.Firstname).ToList());
        }

        public ActionResult Flights()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirplanes"] = _airplaneBLL.GetAllAirplanes();
            TempData["allRoutes"] = _routeBLL.GetAllRoutesConnections();
            return View(_flightBLL.GetAllFlightConnections());
        }

        public ActionResult Routes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirports"] = _airportBLL.GetAllAirports();
            return View(_routeBLL.GetAllRoutes().OrderBy(r => r.FromAirport.Name).ToList());
        }

        [HttpPost]
        public ActionResult UpdateRoute(Route route)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return RedirectToAction("Routes", "Admin");
            }

            string result = _routeBLL.CanUpdateRoute(route);

            if (result.Length > 0)
            {
                SetErrorMessage(result);
            }
            else
            {
                _routeBLL.UpdateRoute(route);
                SetMessage("Route with id " + route.Id + " was successfully updated");
            }

            return RedirectToAction("Routes", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteRoute(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            string message = _routeBLL.CanDeleteRoute(id);

            if (message.Length == 0)
            {
                Route route = _routeBLL.DeleteRoute(id);
                SetMessage("Route with id " + route.Id + " has been deleted.");
            }
            else
            {
                SetErrorMessage(message);
            }

            return RedirectToAction("Routes", "Admin");
        }

        [HttpGet]
        public ActionResult Order(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            Order o = _orderBLL.GetOrder(id);
            if (o != null)
            {
                return View(o);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            //delete customer

            if (!_customerBLL.CanDelete(id))
            {
                SetErrorMessage("This customer has orders connected, and so cannot be deleted!");
                return RedirectToAction("Customers", "Admin");
            }

            Customer customer = _customerBLL.DeleteCustomer(id);
            if (customer != null) {
                SetMessage(customer.Firstname + " " + customer.Lastname + " successfully deleted");
            }
            else
            {
                SetErrorMessage("An error occured");
            }

            return RedirectToAction("Customers", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteAirport(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!_airportBLL.AirportIsUsedInRoutes(id))
            {
                SetErrorMessage("This airport has routes connected and cannot be deleted!");
                return RedirectToAction("Airports", "Admin");
            }

            Airport airport = _airportBLL.DeleteAirport(id);
            if (airport != null)
            {
                SetMessage(airport.Name + " successfully deleted");
            }
            else
            {
                SetErrorMessage("An error occured");
            }
            return RedirectToAction("Airports", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteOrder(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            Order order = _orderBLL.GetOrder(id);

            if (order != null)
            {
                foreach (var ticket in order.Tickets)
                {
                    if (ticket != null)
                    {
                        _ticketBLL.DeleteTicket(ticket.Id);
                    }
                }
                if(order.Customer != null)
                {
                    _customerBLL.DeleteAssociatedOrder(order.Customer.Id, order.Id);
                }
                
                SetMessage(order.Reference + " successfully deleted");
            }
            else
            {
                SetErrorMessage("An error occured");
            }
            return RedirectToAction("Orders", "Admin");
        }

        public ActionResult CreateOrder()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["AllFlights"] = _flightBLL.GetAllFlightsWithFullRoute();
            TempData["AllCustomers"] = _customerBLL.GetAllCustomers();

            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderDTO dto)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            String result = _orderBLL.CanCreateOrder(dto);

            if(result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("CreateOrder", "Admin");
            }

            Order order = _orderBLL.CreateOrder(dto);
            SetMessage("Order with reference " + order.Reference + " was successfully created");

            return RedirectToAction("Orders", "Admin");
        }

        public ActionResult CreateCustomer()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return RedirectToAction("Customers", "Admin");
            }

            //Validate in server!!
            if (customer == null || customer.Firstname == null || customer.Lastname == null || customer.Telephone == null || customer.Email == null)
            {
                SetErrorMessage("All fields must be filled out!");
                return View();
            }
            _customerBLL.AddCustomer(customer);
            SetMessage(customer.Firstname + " " + customer.Lastname + " successfully created");
            return RedirectToAction("Customers", "Admin");
        }

        public ActionResult CreateAirport()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateAirport(Airport airport)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if(ModelState.IsValid)
            {
                _airportBLL.AddAirport(airport);
                SetMessage(airport.Name + " successfully created");
            } else
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
            }

            return RedirectToAction("Airports", "Admin");
        }

        public ActionResult CreateFlight()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirplanes"] = _airplaneBLL.GetAllAirplanes();
            TempData["allRoutes"] = _routeBLL.GetAllRoutes();

            return View();
        }

        [HttpPost]
        public ActionResult CreateFlight(Flight flight)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return RedirectToAction("Flights", "Admin");
            }

            string result = _flightBLL.CanInsertFlight(flight);

            if(result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("CreateFlight", "Admin");
            }

            flight = _flightBLL.InsertFlight(flight);

            SetMessage("Flight created with id: " + flight.Id);

            return RedirectToAction("Flights", "Admin");
        }

        public ActionResult CreateAirplane()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateAirplane(Airplane airplane)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                SetMessage(airplane.Model + " successfully created");
                _airplaneBLL.InsertAirplane(airplane);
                return RedirectToAction("Airplanes", "Admin");
            }
            else
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return View();
            }
        }

        public ActionResult CreateRoute()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirports"] = _airportBLL.GetAllAirports();
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoute(Route route)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return RedirectToAction("Routes", "Admin");
            }

            if (route.FromAirport == null || route.ToAirport == null || route.FlightTime == null)
            {
                SetErrorMessage("All fields must be filled out!");
                return View();
            }

            if(route.FromAirport.Id == route.ToAirport.Id)
            {
                SetErrorMessage("Airports can not be the same!");
                return View();
            }

            Airport fromAirport = _airportBLL.GetAirport(route.FromAirport.Id);
            Airport toAirport = _airportBLL.GetAirport(route.ToAirport.Id);

            _routeBLL.AddRoute(route);
            SetMessage(fromAirport.Name + " - " + toAirport.Name + " on " + route.FlightTime.ToString() + " successfully created");
            return RedirectToAction("Routes", "Admin");
        }

        public ActionResult Orders()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(_orderBLL.GetAllOrdersConnections().OrderBy(o => o.Reference).ToList());
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                _customerBLL.UpdateCustomer(customer);
                SetMessage("Customer with id " + customer.Id + " was successfully updated");
            }
            else
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
            }
            return RedirectToAction("Customers", "Admin");
        }

        [HttpPost]
        public ActionResult UpdateAirport(Airport airport)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                _airportBLL.UpdateAirport(airport);
                SetMessage("Airport " + airport.Name + " was successfully updated");
            }
            else
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
            }
            return RedirectToAction("Airports", "Admin");
        }

        [HttpPost]
        public ActionResult UpdateFlight(Flight flight)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return RedirectToAction("Flights", "Admin");
            }

            string result = _flightBLL.CanUpdateFlight(flight);
            if (result.Length == 0)
            {
                _flightBLL.UpdateFlight(flight);
            }
            else
            {
                SetErrorMessage(result);
            }

            return RedirectToAction("Flights", "Admin");
        }

        [HttpPost]
        public ActionResult UpdateAirplane(Airplane airplane)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                SetErrorMessage(GetErrorFromModel(ModelState));
                return RedirectToAction("Airplanes", "Admin");
            }

            string result = _airplaneBLL.CanUpdateAirplane(airplane);

            if (result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("Airplanes", "Admin");
            }

            airplane = _airplaneBLL.UpdateAirplane(airplane);
            return RedirectToAction("Airplanes", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteFlight(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            string message = _flightBLL.CanDeleteFlight(id);

            if(message.Length == 0)
            {
                Flight flight = _flightBLL.DeleteFlight(id);
                SetMessage("Flight with id " + flight.Id + " with flight at " + flight.Time.ToShortDateString() + " has been deleted.");
            }
            else
            {
                SetErrorMessage(message);
            }

            return RedirectToAction("Flights", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteAirplane(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            string result = _airplaneBLL.CanDeleteAirplane(id);

            if(result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("Airplanes", "Admin");
            }

            Airplane airplane = _airplaneBLL.DeleteAirplane(id);
            SetMessage("Airplane " + airplane.Model + ", with id: " + airplane.Id + " was successfully deleted");
            return RedirectToAction("Airplanes", "Admin");
        }

        public bool UserIsLoggedIn()
        {
            if (Session["loggedIn"] != null)
            {
                if (Session["loggedIn"].Equals(true))
                {
                    return true;
                }
            }
            return false;
        }

        private void SetMessage(string message)
        {
            TempData["message"] = message;
        }

        private void SetErrorMessage(string message)
        {
            TempData["errorMessage"] = message;
        }

        private string GetErrorFromModel(ModelStateDictionary modelState)
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            StringBuilder errors = new StringBuilder();

            foreach (ModelError e in allErrors) errors.Append(e.ErrorMessage).Append(" ");

            return errors.ToString();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            ViewResult result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };

            filterContext.Result = result;

            LogHelper.Log(filterContext.Exception);
            filterContext.ExceptionHandled = true;
        }
    }
}