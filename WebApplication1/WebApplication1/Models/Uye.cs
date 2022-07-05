using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class Uye:IdentityUser<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adres { get; set; }

        //Nav. Props.
        public ICollection<Fatura> Faturalar { get; set; }
        public ICollection<Sepet> Sepetler { get; set; }
    }
}
