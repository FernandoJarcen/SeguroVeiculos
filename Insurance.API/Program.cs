using Insurance.Application;
using Insurance.Domain.Interfaces;
using Insurance.Infrastructure.Extensions;
using Insurance.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

#region Camadas

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

#endregion

#region Infraestrutura

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Serviços externos

var seguradoApiUrl = builder.Configuration["ExternalServices:SeguradoApiUrl"] ?? "http://localhost:3000";
builder.Services.AddHttpClient<ISeguradoService, SeguradoExternalService>(client => {
    client.BaseAddress = new Uri(seguradoApiUrl);
});

#endregion

#region Segurança

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

#endregion

#region Tratamento de Erros Globais

app.UseExceptionHandler(exceptionHandlerApp => {
    exceptionHandlerApp.Run(async context => {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { mensagem = "Ocorreu um erro interno no servidor." });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

#endregion

#region arquivos estaticos

app.UseDefaultFiles();
app.UseStaticFiles();

#endregion

#region Redirecionamento e roteamento

app.UseHttpsRedirection();
app.UseRouting();

#endregion

#region outros

app.UseCors("AllowAll");
app.UseAuthorization();

#endregion

#region endpoints

app.MapControllers();

#endregion

app.Run();
