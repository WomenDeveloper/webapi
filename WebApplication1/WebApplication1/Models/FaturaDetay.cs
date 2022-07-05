namespace WebApplication1.Models
{
    public class FaturaDetay
    {
        public int FaturaDetayID { get; set; }
        public int FaturaID { get; set; }

        public int UrunID { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }


        public Fatura? Fatura { get; set; }
        public Urun Urun { get; set; }
    }
}
