using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Areas.Uyeler.Controllers
{
    [Authorize(Roles ="Uye")]
    [Area("Uyeler")]
    public class PanelController : Controller
    {
        private readonly UserManager<Uye> _userManager;
        private readonly ApplicationDbContext _db;

        public PanelController(UserManager<Uye> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db; 
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SepeteEkle(int id)
        {
            _db.Sepetler.Add(new Sepet() { UrunID=id, UyeID=GetUserID(), Adet=1 });
            _db.SaveChanges();
            return Redirect("~/Urun");
           // return Content(id+ " UyeID " + GetUserID());
        }

        private int GetUserID()
        {
            return Convert.ToInt32(_userManager.GetUserId(User));
        }

        public IActionResult SepetGoster()
        {
            int uyeID = GetUserID();
            var sepetDetaylari = _db.Sepetler.Include("Urun").Where(s => s.UyeID == uyeID).ToList();
            return View(sepetDetaylari);
        }

        [HttpPost]
        public IActionResult SatinAl() 
        {
            //sepetteki verileri al
            //fatura kes
            //fatura detaya yaz
            //stoktan dus
            //sepeti temizle

            int uyeID = GetUserID();
            var sepetDetaylari = _db.Sepetler.Include("Urun").Where(s => s.UyeID == uyeID).ToList();

            //Fatura tablosuna ekle => FaturaID'yi al...
            Fatura fatura = new Fatura { UyeID = uyeID, FaturaKesimTarihi = DateTime.Now };
            _db.Faturalar.Add(fatura);
            _db.SaveChanges();

            int faturaID = fatura.FaturaID;

            foreach (Sepet item in sepetDetaylari)
            {
                //Detay tansolusna ekle
                _db.FaturaDetaylari.Add(new FaturaDetay() { 
                  FaturaID= faturaID,
                  UrunID =item.UrunID,
                   Adet=item.Adet,
                   Fiyat = item.Urun.Fiyat,
                   Tutar = item.Urun.Fiyat* item.Adet
                });

                //Stoktan düş
                item.Urun.StokAdedi -= item.Adet; 
            }

            _db.Sepetler.RemoveRange(sepetDetaylari);

            _db.SaveChanges();

            return View();
        }
    }
}
