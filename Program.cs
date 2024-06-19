using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using PageSage.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ContextConnection") ?? throw new InvalidOperationException("Connection string 'ContextConnection' not found.");


builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
   // .AddEntityFrameworkStores<Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient(); //Bu metot, HttpClient nesnelerini başlatmak, yapılandırmak ve uygun bir şekilde yönetmek için arkadaki servislerle ilgilenir. 

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(policy =>
{
	policy.AllowAnyOrigin();
	policy.AllowAnyMethod();
	policy.AllowAnyHeader();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GoogleBookService}/{action=SearchBooks}/{id?}");

app.Run();
