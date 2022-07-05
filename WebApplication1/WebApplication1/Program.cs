using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<Uye>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddIdentity<Uye, Rol>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Rol>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddMvc();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



//Areas larýn kullanýlabilmesi için gerekli ayarlar(Admin, Uyeler)
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Uyeler",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});





app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Urun}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
