using Assessment6.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Assessment6.Controllers
{
    public class APIController : Controller
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

        public ActionResult ViewRawData()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://www.anapioficeandfire.com/api/characters?page=3&pageSize=20");
            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());
                ViewBag.RawData = data.ReadToEnd();
            }
            var characters = GetCharacterList();
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

        public List<APICharacter> GetCharacterList()
        {
            var characters = new List<APICharacter>();

            var petyr = new APICharacter();
            var jon = new APICharacter();
            var sansa = new APICharacter();
            var brienne = new APICharacter();
            var danny = new APICharacter();

            petyr.Name = GetName("Petyr Baelish");
            sansa.Name = GetName("Sansa Stark");
            jon.Name = GetName("Jon Snow");
            brienne.Name = GetName("Brienne of Tarth");
            danny.Name = GetName("Daenerys Targaryen");

            //jon.Allegiance = GetAllegiance("362");
            //sansa.Allegiance = GetAllegiance("362");
            //danny.Allegiance = GetAllegiance("378");
            //petyr.Allegiance = GetAllegiance("10");
            //brienne.Allegiance = GetAllegiance("17");

            characters.Add(petyr);
            characters.Add(sansa);
            characters.Add(jon);
            characters.Add(brienne);
            characters.Add(danny);

            return characters;
        }
    }
}