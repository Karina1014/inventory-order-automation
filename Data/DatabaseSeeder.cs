using Microsoft.EntityFrameworkCore;
using InventoryFlow.Models;

namespace InventoryFlow.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AplicationDbContext context)
    {
        // 1. Seed StoreTypes (si no existen)
        if (!await context.StoreTypes.AnyAsync())
        {
            var storeTypes = new List<StoreType>
            {
                new()
                {
                    Name = "Clínica Veterinaria Integral",
                    Description = "Atención médica general y especializada, hospitalización, cirugías y farmacia.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Consultorio de Atención Primaria",
                    Description = "Consultas de rutina, vacunación, desparasitación y procedimientos ambulatorios básicos.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Pet Shop y Guardería",
                    Description = "Venta de alimentos y accesorios para mascotas con servicio de cuidado diurno.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                }
            };

            await context.StoreTypes.AddRangeAsync(storeTypes);
            await context.SaveChangesAsync();
        }

        // 2. Seed Stores (si no existen)
        if (!await context.Stores.AnyAsync())
        {
            var hospitalType = await context.StoreTypes.FirstOrDefaultAsync(t => t.Name == "Clínica Veterinaria Integral");
            var primaryCareType = await context.StoreTypes.FirstOrDefaultAsync(t => t.Name == "Consultorio de Atención Primaria");
            var petShopType = await context.StoreTypes.FirstOrDefaultAsync(t => t.Name == "Pet Shop y Guardería");

            if (hospitalType != null && primaryCareType != null && petShopType != null)
            {
                var stores = new List<Store>
                {
                    new()
                    {
                        Name = "Med Vet - Galo Plaza (Matriz)",
                        Description = "Atención continua 24h. Medicina general, hospitalización y cirugías. Ubicación: Galo Plaza Lasso y diag. al Estadio N°15-396, Calderón.",
                        IsActive = true,
                        CreationDate = DateTime.UtcNow,
                        IdStoreType = hospitalType.IdStoreType
                    },
                    new()
                    {
                        Name = "Med Vet - Llano Grande",
                        Description = "Atención ambulatoria diurna, vacunación y procedimientos básicos. Ubicación: Av. Gabriel García Moreno y Pje. Orquídeas.",
                        IsActive = true,
                        CreationDate = DateTime.UtcNow,
                        IdStoreType = primaryCareType.IdStoreType
                    },
                    new()
                    {
                        Name = "Med Vet - Pet Shop & Guardería",
                        Description = "Venta de alimentos, accesorios y servicio de guardería canina.",
                        IsActive = true,
                        CreationDate = DateTime.UtcNow,
                        IdStoreType = petShopType.IdStoreType
                    }
                };

                await context.Stores.AddRangeAsync(stores);
                await context.SaveChangesAsync();
            }
        }
    }
}
