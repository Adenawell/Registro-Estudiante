using Microsoft.EntityFrameworkCore;
using Registro_Estudiante.Components;
using Registro_Estudiante.DAL;
using Registro_Estudiante.Services;
using Registro_Asignaturas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Usamos una sola cadena de conexión para evitar el error de ConnectionString.
// Asegúrate de que "SqlAzureConnection" sea el nombre que tienes en tu appsettings.json
var connectionString = builder.Configuration.GetConnectionString("SqlConStr");

builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<EstudiantesServices>();

// AÑADE ESTA LÍNEA AQUÍ:
builder.Services.AddScoped<AsignaturasServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();