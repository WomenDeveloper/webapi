using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPI.Models;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public IActionResult Get()
        {
            List<Urun> urunler = new List<Urun>()
            {
                new Urun{ UrunID=1, UrunAdi="Urun 1", Fiyat=33 },
                new Urun{ UrunID=2, UrunAdi="Urun 2", Fiyat=12 },
                new Urun{ UrunID=3, UrunAdi="Urun 3", Fiyat=65 }
            };

            return Ok(urunler);
            //return NotFound();
            //return Unauthorized();
        }
    }
}
