using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using WebAPI.Models;
namespace WebAPI.Controllers
{
    public class ServisController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient istemci = new HttpClient();

            var user = new { Username = "Cevdo", Password = "123" };

           var result= await istemci.PostAsJsonAsync("https://localhost:7160/api/Film/user", user);

            string token = null;
            if(result.StatusCode== System.Net.HttpStatusCode.OK)
              token = await result.Content.ReadAsStringAsync();

            string json = "";
            if (token != null)
            {
                istemci.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
             json= await istemci.GetStringAsync("https://localhost:7160/api/Film");
            }
            List<FilmVM> filmler= JsonConvert.DeserializeObject<List<FilmVM>>(json);

            //return Content(json);
            return View(filmler);
        }
    }
}
