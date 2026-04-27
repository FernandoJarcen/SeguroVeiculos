using Insurance.Application;
using Insurance.Application.Services;
using Insurance.Domain.Interfaces;
using Insurance.Infrastructure;
using Insurance.Infrastructure.Context;
using Insurance.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<InsuranceDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Insurance.Infrastructure")));

builder.Services.AddScoped<ISeguroRepository, SeguroRepository>();
builder.Services.AddScoped<SeguroService>();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var seguradoApiUrl = builder.Configuration["ExternalServices:SeguradoApiUrl"] ?? "http://localhost:3000";

builder.Services.AddHttpClient<ISeguradoService, SeguradoExternalService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:3000/"); // URL do seu JSON Server/Mock
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
