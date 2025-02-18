using AdminExpedientePersonal.Repository;
using AdminExpedientePersonal.Repository.DatabaseConnection;
using AdminExpedientePersonal.Services;
using AdminExpedientePersonal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;  // Protege la sesión contra ataques XSS
    options.Cookie.IsEssential = true; // Asegura que la cookie de sesión siempre se envíe
});

builder.Services.AddSingleton<IEventBus,EventBus>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IBitacoraService, BitacoraService>();
builder.Services.AddScoped<IInstitucionRepository, InstitucionRepository>();
builder.Services.AddScoped<IInstitucionService, InstitucionService>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
    var bitacoraService = scope.ServiceProvider.GetRequiredService<IBitacoraService>();

    eventBus.Subscribe<BitacoraEvent>(async (bitacoraEvent) =>
        await bitacoraService.RegistrarBitacora(bitacoraEvent));
}
app.MapRazorPages();

app.Run();
