using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
namespace WebApplication1.Controllers
{
    public class UrunController : Controller
    {
        ApplicationDbContext _db;
        public UrunController(ApplicationDbContext db)
        {
            _db = db;
            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();
        }
        public IActionResult Index()
        {
            return View(_db.Urunler.Include("Kategori").ToList());
        }
    }
}
