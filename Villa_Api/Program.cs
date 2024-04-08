

using Villa_Api.Logger;

var builder = WebApplication.CreateBuilder(args);
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
builder.Services.AddSingleton<ILogging, Logv2>();

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
