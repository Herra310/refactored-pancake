using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;

namespace PruebaIngreso.Controllers
{

    public class MarginController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        // Selecciona un código aleatorio
        private static readonly string[] statusCodes = { "E-U10-UNILATIN" , "E-U10-DSCVCOVE", "E-E10-PF2SHOW" };
        private static readonly Random random = new Random();

        public async Task<ActionResult> GetMargin()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //
                //usar un codigo aleatorio a manera de ejemplo para la prueba
                string Code = statusCodes[random.Next(statusCodes.Length)];
                string apiUrl = $"https://refactored-pancake.free.beeceptor.com/margin/{Code}";

                HttpResponseMessage response = await client.GetAsync(apiUrl); //Se consume el API

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(responseBody);
                    var margin = jsonObject["margin"].ToString();

                    ViewBag.Margin = margin; //si el codigo es 200 muestra esto
                }
                else
                {
                    ViewBag.Margin = $" 0.0"; //si el codigo es diferente de 200 muestra esto
                }

                return View("~/Views/Home/Test3.cshtml"); //el path de la vista
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}