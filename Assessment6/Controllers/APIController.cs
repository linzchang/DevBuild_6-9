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
            var characters = GetNames("Petyr Baelish");
            return View(characters);
        }

        public List<APICharacter> GetNames(string name)
        {
            var characters = new List<APICharacter>();

            HttpWebRequest request = WebRequest.CreateHttp($"https://www.anapioficeandfire.com/api/characters?Name={name}");
            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                var serializer = new JsonSerializer();

                //JObject dataObject = JObject.Parse(data.ReadToEnd());
                //var petyr = new APICharacter();
                //petyr.Name = dataObject["name"];

                using (StreamReader data = new StreamReader(response.GetResponseStream()))
                using (var jsonReader = new JsonTextReader(data))
                {
                    characters = serializer.Deserialize<List<APICharacter>>(jsonReader);
                }

            }

            return characters;
        }
    }
}