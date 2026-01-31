using Microsoft.EntityFrameworkCore;
using Registro_Estudiante.Components;
using Registro_Estudiante.DAL;
using Registro_Estudiante.Services;
using Registro_TiposPuntos.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("SqlConStr");

builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<EstudiantesServices>();
builder.Services.AddScoped<TiposPuntosServices>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();