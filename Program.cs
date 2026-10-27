using Microsoft.EntityFrameworkCore;
using InventoryFlow.Repository;
using InventoryFlow.Mapping;
using InventoryFlow.Components;
using InventoryFlow.Data;

var builder = WebApplication.CreateBuilder(args);


// 1. Configuración de la base de datos
var dbConnectionString = builder.Configuration.GetConnectionString("ConexionSql");
builder.Services.AddDbContext<AplicationDbContext>(options => 
    options.UseSqlServer(dbConnectionString));

// 2. Registro de AutoMapper
builder.Services.AddAutoMapper(typeof(StoreProfile));

// 4. Inyección de Repositorios
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();

// 5. Configuración de Controladores y OpenAPI (Web API)
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 6. Configuración de Blazor (Renderizado Interactivo del Servidor)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Ejecución de datos semilla
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
    await DatabaseSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseAuthorization();

app.MapControllers();

// Mapeo del enrutador raíz de Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
