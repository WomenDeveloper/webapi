namespace WebApplication1.Models
{
    public class Sepet
    {
        public int SepetID { get; set; }
        //Kime ait UyeID
        public int UyeID { get; set; }
        public int UrunID { get; set; }
        public int Adet { get; set; }

        //Nav. Property
        public Uye Uye { get; set; }
        public Urun? Urun { get; set; }

    }
}
