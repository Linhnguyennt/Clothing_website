using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using week05_PTW.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DB") ?? throw new InvalidOperationException("Connection string 'week05_PTWContextConnection' not found.");

builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<week05_PTWContext>(options =>
//    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WebDbContext>();

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<WebDbContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//  .AddEntityFrameworkStores<WebDbContext>();

// Add services to the container.


builder.Services.ConfigureApplicationCookie(
    options =>
    {
        options.LoginPath = "/User/Login";
    });


builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "Weblab";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                                            //cfg.IdleTimeout = new TimeSpan(0, 30, 0);
                                            // Thời gian tồn tại của Session
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
 );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
