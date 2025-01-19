using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Services;
using CityPOS.Services.ReportingServices;
using CityPOS.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddControllersWithViews();
var config=builder.Configuration;
builder.Services.AddDbContext<CityPOSDbContext>(o => o.UseSqlServer(config.GetConnectionString("CityPOSConnectionstring")));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUnitService, UnitService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<ITownshipService, TownshipService>();
builder.Services.AddTransient<ISupplierService, SupplierService>();
builder.Services.AddTransient<ICustomerService, CustomerService>(); 
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IPriceService, PriceService>();
builder.Services.AddTransient<IStockBalanceService, StockBalanceService>();
builder.Services.AddTransient<IStockLedgerService, StockLedgerService>();
builder.Services.AddTransient<IPurchaseService, PurchaseService>();
builder.Services.AddTransient< IPurchaseDetailService, PurchaseDetailService>();
builder.Services.AddTransient<ISaleOrderService, SaleOrderService>();
builder.Services.AddTransient<ISaleDetailService, SaleDetailService>();
builder.Services.AddTransient<ISaleDetailReportService, SaleDetailReportService>();
builder.Services.AddTransient<IPurchaseDetailReportService,PurchaseDetailReportService>();
builder.Services.AddTransient<IProfitReportService, ProfitReportService>();
builder.Services.AddTransient<IAspNetRolesService, AspNetRolesService>();

builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CityPOSDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


//async Task SeedRolesAndUsers(IServiceProvider services)
//{
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

   
//    var roles = new[] { "Admin", "Manager", "Cashier" };
//    var users = new[]
//    {
//        new { Email = "admin@citypos.com", Password = "Admin@123", Role = "Admin" },
//        new { Email = "manager@citypos.com", Password = "Manager@123", Role = "Manager" },
//        new { Email = "cashier@citypos.com", Password = "Cashier@123", Role = "Cashier" }
//    };

  
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }

  
//    foreach (var user in users)
//    {
//        var existingUser = await userManager.FindByEmailAsync(user.Email);
//        if (existingUser == null)
//        {
//            var newUser = new IdentityUser { UserName = user.Email, Email = user.Email, EmailConfirmed = true };
//            var createUserResult = await userManager.CreateAsync(newUser, user.Password);

//            if (createUserResult.Succeeded)
//            {
//                await userManager.AddToRoleAsync(newUser, user.Role);
//            }
//        }
//    }
//}

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    await SeedRolesAndUsers(scope.ServiceProvider);
//}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
