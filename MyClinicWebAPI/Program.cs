using Microsoft.EntityFrameworkCore;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Repository;
using MyClinicWebAPI.DataLayer;
using System.Reflection;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IClient, ClientRepository>();
builder.Services.AddScoped<IPrescription, PrescriptionRepository>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen( c => 
{ 
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title= "MyClinic Api",
        Version = "v1",
        Description = "An API to get data from MyClinic database",
        //TermsOfService = "",
        Contact = new OpenApiContact
        {
            Name = "Paulo Silva",
            Email = "devpmms81@gmail.com"
            //Url = new Uri("https://mysite.com") 
        } /*,
        License = new OpenApiLicense {
            Name = "MyClinic API LICX",
            Url = new Uri("https://mysite.com/license")
        }
        */
    });

    // Define XML file documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // Configuration to include comments in API methods
    c.IncludeXmlComments(xmlPath); 
});


var connectionString = builder.Configuration.GetConnectionString("AppDBConnectionString");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    if (connectionString != null)
        options.UseMySQL(connectionString);
    else throw new NullReferenceException("ConnectionString not defined");
    //throw new Exception("ConnectionString not defined");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
