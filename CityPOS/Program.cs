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

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<CityPOSDbContext>()
//    .AddDefaultUI()
//    .AddDefaultTokenProviders();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configure password requirements
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.SignIn.RequireConfirmedEmail = false;
    // Configure lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    // Configure user settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<CityPOSDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();



var app = builder.Build();
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
