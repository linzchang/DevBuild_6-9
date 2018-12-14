using Assessment6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            string userEmail = User.Identity.Name;
            
            PartyDbEntities ORM = new PartyDbEntities();
            Guest found = ORM.Guests.Find(userEmail);

            if (guest.Attending != null)
            {
                found.Attending = guest.Attending;
                found.PartyDate = guest.PartyDate;
                found.PlusOne = guest.PlusOne;
                found.PlusOneName = guest.PlusOneName;

                ORM.Entry(found).State = EntityState.Modified;
                ORM.SaveChanges();
                return View(guest);
            }

            return View(guest);
        }

        public ActionResult Dish()
        {
            string userEmail = User.Identity.Name;

            PartyDbEntities ORM = new PartyDbEntities();
            ViewBag.DishList = ORM.Dishes.Where(x => x.Email == userEmail).ToList();

            return View();
        }

        public ActionResult Dish_Confirmation(Dish dish)
        {
            return View(dish);
        }
    }
}