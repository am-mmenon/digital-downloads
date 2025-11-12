using DigitalDownloads.Core.Config;
using DigitalDownloads.Core.Data;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddControllersWithViews();   // <-- this enables MVC controllers
builder.Services.AddRazorPages();             // optional, for Razor Pages

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DigitalDownloads.Web"))
    );

var stripeSettings = builder.Configuration.GetSection("Stripe");
StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
builder.Services.Configure<StripeSettings>(stripeSettings);

var app = builder.Build();

//Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // important for serving CSS, JS, etc.

app.UseRouting();

app.UseAuthorization();

//Make sure controllers are mapped correctly
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // optional if you use Razor pages too

app.Run();
