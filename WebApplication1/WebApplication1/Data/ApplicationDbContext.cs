using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebApplication1.Models;
using WebApplication1.Models.Configuration;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<Uye,Rol,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Sepet> Sepetler { get; set; }
        public DbSet<Fatura> Faturalar { get; set; }
        public DbSet<FaturaDetay> FaturaDetaylari { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Rol> Roller { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<Rol>(new RolCFG());
        }
    }
}