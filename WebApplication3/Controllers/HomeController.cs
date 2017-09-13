using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        DB db = new DB();
        public ActionResult Index()
        {
            var instance = db.Customers.Find(1);
            return View(instance);
        }
    }
}