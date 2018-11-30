using Assessment6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RSVP()
        {
            return View();
        }

        public ActionResult RSVP_Confirmation(Guest guest)
        {
            return View(guest);
        }

        public ActionResult Dish()
        {
            return View();
        }

        public ActionResult Dish_Confirmation(Dish dish)
        {
            return View(dish);
        }
    }
}