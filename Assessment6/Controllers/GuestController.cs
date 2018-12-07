using Assessment6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment6.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult AddGuest()
        {
            return View();
        }

        public ActionResult SaveNewGuest(Guest newGuest)
        {
            PartyDbEntities ORM = new PartyDbEntities();

            if (newGuest != null)
            {
                ORM.Guests.Add(newGuest);
                ORM.SaveChanges();
            }

            return RedirectToAction("RSVP", "Home");
        }

        public ActionResult EditGuest(int guestID)
        {
            PartyDbEntities ORM = new PartyDbEntities();
            Guest found = ORM.Guests.Find(guestID);

            return View(found);
        }

        public ActionResult SaveGuestChanges(Guest updatedGuest)
        {
            PartyDbEntities ORM = new PartyDbEntities();
            Guest oldGuest = ORM.Guests.Find(updatedGuest.GuestID);
            oldGuest.FirstName = updatedGuest.FirstName;
            oldGuest.LastName = updatedGuest.LastName;
            oldGuest.Email = updatedGuest.Email;
            oldGuest.Attending = updatedGuest.Attending;
            oldGuest.PartyDate = updatedGuest.PartyDate;
            oldGuest.PlusOne = updatedGuest.PlusOne;
            oldGuest.PlusOneName = updatedGuest.PlusOneName;

            ORM.Entry(oldGuest).State = EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("RSVP", "Home");
        }

        public ActionResult DeleteGuest(int guestID)
        {
            PartyDbEntities ORM = new PartyDbEntities();
            Guest found = ORM.Guests.Find(guestID);

            ORM.Guests.Remove(found);
            ORM.SaveChanges();
            return RedirectToAction("RSVP", "Home");
        }
    }
}