namespace WebApplication1.Models
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public int StokAdedi { get; set; }

        public int KategoriID { get; set; }

        //Navigation Property
        public Kategori? Kategori { get; set; }
        public ICollection<FaturaDetay>? FaturaDetaylari { get; set; }
        public ICollection<Sepet>? Sepetler { get; set; }
    }
}
