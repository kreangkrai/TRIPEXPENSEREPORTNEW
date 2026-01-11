using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAllowance, AllowanceService>();
builder.Services.AddTransient<IUser, UserService>();
builder.Services.AddTransient<IArea, AreaService>();
builder.Services.AddTransient<ITrip, TripService>();
builder.Services.AddTransient<IProvince, ProvinceService>();
builder.Services.AddTransient<ICar, CarService>();
builder.Services.AddTransient<IService, ServiceService>();
builder.Services.AddTransient<IEmployee, EmployeeService>();
builder.Services.AddTransient<IPersonal, PersonalService>();
builder.Services.AddTransient<IGasoline, GasolineService>();
builder.Services.AddTransient<TRIPEXPENSEREPORT.CTLInterfaces.IHoliday, TRIPEXPENSEREPORT.CTLServices.HolidayService>();
builder.Services.AddTransient<TRIPEXPENSEREPORT.CTLInterfaces.IEmployee, TRIPEXPENSEREPORT.CTLServices.EmployeeService>();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(120);
});

var app = builder.Build();

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

app.UseCookiePolicy();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
