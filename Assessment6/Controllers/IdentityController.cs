using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assessment6.Models;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Assessment6.Controllers
{
    public class IdentityController : Controller
    {
        public UserManager<IdentityUser> userManager =>
            HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationModel newUser)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = newUser.Email,
                };

                var IdentityResult = await userManager.CreateAsync(user, newUser.Password);
                if (IdentityResult.Succeeded)
                {
                    PartyDbEntities ORM = new PartyDbEntities();
                    Guest addGuest = new Guest()
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Attending = newUser.Attending,
                        PartyDate = newUser.PartyDate,
                        PlusOne = newUser.PlusOne,
                        PlusOneName = newUser.PlusOneName
                    };

                    ORM.Guests.Add(addGuest);
                    ORM.SaveChanges();
                    return RedirectToAction("Login", newUser);
                }

            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;

                IdentityUser user = userManager.Find(login.Email, login.Password);

                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false}, ident);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid email or password.");

            return View(login);
        }
    }
}