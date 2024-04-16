



using Microsoft.EntityFrameworkCore;
using Villa_Api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.
//    File("log/villalogs.txt", rollingInterval: RollingInterval.Day).CreateBootstrapLogger();

//builder.Host.UseSerilog(); 

// Add services to the container.

builder.Services.AddControllers(options =>
{
   // options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

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
