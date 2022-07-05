namespace WebApplication1.Models
{
    public class Fatura
    {
        public int FaturaID { get; set; }
        public DateTime FaturaKesimTarihi { get; set; } = DateTime.Now;

        //UyeID
        public int UyeID { get; set; }

        //Navigation Props...
        public Uye Uye { get; set; }
        public ICollection<FaturaDetay> FaturaDetaylari { get; set; }
       
    }
}
