using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyClinic.DataLayer;
using MyClinic.Interfaces;
using MyClinic.Repository;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IClient, ClientRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.AccessDeniedPath = "/Account/AccessDenied"; // We need to create the page
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminProfile", policy => policy.RequireClaim("TypeUser", "Admin"));
    /*
     * options.AddPolicy("SuperAdmin",policy => policy
     *          .RequireClaim("TypeUser","Admin")
     *          .RequireClaim("WorkingGroup","Testers"));
     */
});

var connectionString = builder.Configuration.GetConnectionString("AppDBConnectionString");

builder.Services.AddDbContext<ApplicationDBContext> (options =>
{
    if (connectionString != null)
        options.UseMySQL(connectionString);
    else throw new NullReferenceException("ConnectionString not defined");
        //throw new Exception("ConnectionString not defined");
});

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

//Create.CreateClient(app); -> Run only at first running to populate DB

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseRouting();

app.UseAuthentication(); // Insert Authentication Middleware

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.UseStatusCodePagesWithRedirects("/Error/{0}"); //Added, point to error page

app.Run();
