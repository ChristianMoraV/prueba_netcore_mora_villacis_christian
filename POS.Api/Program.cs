using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Extensions;
using POS.Infraestructure.Extensions;
using POS.Infraestructure.Persistences.Contexts;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;



// Add services to the container.

var Cors = "Cors";


builder.Services.AddInjectionInfraestructure(Configuration);
builder.Services.AddInjectionApplication(Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

//servicio para el procedure
//builder.Services.AddDbContext<POSCineContext>(options =>
  //  options.UseSqlServer(builder.Configuration.GetConnectionString("POSConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors
        , builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();

        });
});

var app = builder.Build();
app.UseCors(Cors); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
