using Assessment6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment6.Controllers
{
    [Authorize]
    public class DishController : Controller
    {
        public ActionResult AddDish()
        {
            return View();
        }

        public ActionResult SaveNewDish(Dish newDish)
        {
            string userEmail = User.Identity.Name;
            newDish.Email = userEmail;
            PartyDbEntities ORM = new PartyDbEntities();

            if (newDish != null)
            {
                ORM.Dishes.Add(newDish);
                ORM.SaveChanges();
            }

            return RedirectToAction("Dish", "Home");
        }

        public ActionResult EditDish(int dishID)
        {
            PartyDbEntities ORM = new PartyDbEntities();
            Dish found = ORM.Dishes.Find(dishID);

            return View(found);
        }

        public ActionResult SaveDishChanges(Dish updatedDish)
        {
            PartyDbEntities ORM = new PartyDbEntities();
            Dish oldDish = ORM.Dishes.Find(updatedDish.DishID);
            oldDish.DishName = updatedDish.DishName;
            oldDish.Description = updatedDish.Description;
            oldDish.Category = updatedDish.Category;
            oldDish.GuestName = updatedDish.GuestName;
            oldDish.PhoneNumber = updatedDish.PhoneNumber;

            ORM.Entry(oldDish).State = EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("Dish", "Home");
        }

        public ActionResult DeleteDish(int dishID)
        {
            PartyDbEntities ORM = new PartyDbEntities();
            Dish found = ORM.Dishes.Find(dishID);

            ORM.Dishes.Remove(found);
            ORM.SaveChanges();
            return RedirectToAction("Dish", "Home");
        }
    }
}