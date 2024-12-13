using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EDevletSimulasyon.Areas.MesajEntegrasyon.Controllers
{
    
    public class MesajController : Controller
    {
        private const string FakeEDevletApiBaseUrl = "https://localhost:7103/api/fakeedevlet"; // FakeEDevletAPI URL
        private const string ClientId = "your-client-id";
        private const string ClientSecret = "your-client-secret";
        private const string RedirectUri = "https://localhost:44357/Mesaj/Callback";

        public ActionResult AuthorizeFromFakeEDevletAPI()
        {
            // Prepare the FakeEDevletAPI Authorize URL
            string authorizeUrl = $"{FakeEDevletApiBaseUrl}/authorize?response_type=code&client_id={ClientId}&redirect_uri={RedirectUri}";

            // Redirect the user to the FakeEDevletAPI authorization page
            return Redirect(authorizeUrl);
            
        }
        public ActionResult Login()
        {
            return View("~/Areas/MesajEntegrasyon/Views/Mesaj/Login.cshtml");
        }
        // GET: MesajEntegrasyon/Mesaj
        public ActionResult Index()
        {
            if (Session["AccessToken"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.AccessToken = Session["AccessToken"];
            return View("~/Areas/MesajEntegrasyon/Views/Mesaj/Index.cshtml");

        }

        public async Task<ActionResult> Callback(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new HttpStatusCodeResult(400, "Authorization code is missing.");
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(FakeEDevletApiBaseUrl +"/");//Sona eklenen slash gerekli.

                var tokenRequest = new
                {
                    grantType = "authorization_code",
                    code = code,
                    redirectUri = RedirectUri,//"https://localhost:44300/Mesaj/Callback",
                    clientId = "your-client-id",
                    clientSecret = "your-client-secret"
                };

                // JSON serileştirme
                string json = JsonConvert.SerializeObject(tokenRequest);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("token", content);

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult((int)response.StatusCode, "Failed to retrieve access token.");
                }

                string responseContent = await response.Content.ReadAsStringAsync();

                // Gelen JSON'u deserialize etme
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Token'i saklama
                Session["AccessToken"] = tokenResponse.AccessToken;

                return RedirectToAction("Index");
            }
        }
    }
}