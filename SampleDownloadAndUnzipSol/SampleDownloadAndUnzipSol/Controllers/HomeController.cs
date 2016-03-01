using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.Exchange.WebServices.Data;
using System.Threading;

namespace SampleDownloadAndUnzipSol.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WebProxy proxy = new WebProxy("proxy.agl.int:8080");
            proxy.Credentials = CredentialCache.DefaultCredentials;  //These can be replaced by user input, if wanted.
            proxy.UseDefaultCredentials = false;
            proxy.BypassProxyOnLocal = false;
            Models.GenderPetList ownersGenderAndPetList = new Models.GenderPetList();
            
            try
            {
                using (var client = new WebClient())
                {
                    //client.Proxy = proxy;
                    var json = client.DownloadString("http://agl-developer-test.azurewebsites.net/people.json");
                    var serializer = new JavaScriptSerializer();            

                    var finalList = serializer.Deserialize<List<Models.Owner>>(json);
                    finalList.RemoveAll(c => c.Pets == null);

                    var flatList = finalList.SelectMany(b => b.Pets.Select(p => new Models.GenderPet {OwnerGender = b.Gender ,PetName = p.Name, PetType=p.Type })).ToList();
                    flatList.RemoveAll(c => c.PetType.ToLower() != "cat");

                    ownersGenderAndPetList.GenderPets = flatList.OrderBy(c=>c.PetName).ToList();
                }

            }
            catch (WebException ex)
            {
                throw;
            }

            return View(ownersGenderAndPetList);
        }
    }


}