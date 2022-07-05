using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("servis/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public string Deneme(int id)
        {
            return "Deneme 123..."+ id;
        }
        [HttpGet("{id:alpha}")]
        public string Emened(string id)
        {
            return "Emened 123..." + id;
        }
    }
}
