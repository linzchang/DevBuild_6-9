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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assessment6.Controllers
{
    public class IdentityController : Controller
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

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
                    UserName = newUser.Email
                };

                var IdentityResult = await userManager.CreateAsync(user, newUser.Password);
                if (IdentityResult.Succeeded)
                {
                    PartyDbEntities ORM = new PartyDbEntities();
                    Guest addGuest = new Guest()
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email
                    };

                    string name = GetName(newUser.Character);
                    string allegiance = "";
                    if (name == "Jon Snow" || name == "Sansa Stark")
                    {
                        allegiance = "362";
                    }
                    else if (name == "Petyr Baelish")
                    {
                        allegiance = "10";
                    }
                    else if (name == "Brienne of Tarth")
                    {
                        allegiance = "17";
                    }
                    else
                    {
                        allegiance = "378";
                    }

                    allegiance = GetAllegiance(allegiance);

                    Character addCharacter = new Character()
                    {
                        Name = name,
                        Allegiance = allegiance,
                        Email = newUser.Email
                    };

                    ORM.Guests.Add(addGuest);
                    ORM.Characters.Add(addCharacter);
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

        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewProfile()
        {
            PartyDbEntities ORM = new PartyDbEntities();
            var characters = ORM.Characters.Where(c => c.Email == User.Identity.Name).ToList();
            return View(characters);
        }

        public string GetName(string name)
        {
            var characters = new List<APICharacter>();

            HttpWebRequest request = WebRequest.CreateHttp($"https://www.anapioficeandfire.com/api/characters?Name={name}");
            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                var serializer = new JsonSerializer();

                using (StreamReader data = new StreamReader(response.GetResponseStream()))
                using (var jsonReader = new JsonTextReader(data))
                {
                    characters = serializer.Deserialize<List<APICharacter>>(jsonReader);
                }

            }

            return characters[0].Name;
        }

        public string GetAllegiance(string allegiance)
        {
            string houseName = "";

            HttpWebRequest request = WebRequest.CreateHttp($"https://www.anapioficeandfire.com/api/houses/{allegiance}");
            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());
                string JsonData = data.ReadToEnd();
                JObject allegianceData = JObject.Parse(JsonData);
                houseName = (string)allegianceData.SelectToken("name");
            }

            return houseName;
        }
    }
}