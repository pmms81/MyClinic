using Microsoft.EntityFrameworkCore;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Repository;
using MyClinicWebAPI.DataLayer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IClient, ClientRepository>();
builder.Services.AddScoped<IPrescription, PrescriptionRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
