using Assessment6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment6.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
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
            PartyDbEntities ORM = new PartyDbEntities();

            if (guest.FirstName != null)
            {
                ORM.Guests.Add(guest);
                ORM.SaveChanges();
            }

            return View(guest);
        }

        public ActionResult Guests()
        {
            PartyDbEntities ORM = new PartyDbEntities();
            ViewBag.GuestList = ORM.Guests.ToList();

            return View();
        }

        public ActionResult Dish()
        {
            PartyDbEntities ORM = new PartyDbEntities();
            ViewBag.DishList = ORM.Dishes.ToList();

            return View();
        }

        public ActionResult Dish_Confirmation(Dish dish)
        {
            return View(dish);
        }
    }
}