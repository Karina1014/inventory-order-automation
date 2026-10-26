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

        // 3. Seed Categories (si no existen)
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new()
                {
                    Name = "Farmacología y Medicamentos",
                    Description = "Antibióticos, analgésicos, antiinflamatorios y anestésicos para uso clínico.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Vacunas y Biológicos",
                    Description = "Inmunizaciones virales y bacterianas con cadena de frío para caninos y felinos.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Antiparasitarios",
                    Description = "Pipetas, comprimidos masticables y collares contra parásitos internos y externos.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Alimento y Nutrición",
                    Description = "Dietas veterinarias medicadas, alimentos secos y balanceados premium.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                },
                new()
                {
                    Name = "Material Quirúrgico e Insumos",
                    Description = "Jeringas, catéteres, gasas, sueros y material estéril de curación y cirugía.",
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        // 4. Seed Products (si no existen)
        if (!await context.Products.AnyAsync())
        {
            var catFarma = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Farmacología y Medicamentos");
            var catVacunas = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Vacunas y Biológicos");
            var catAntiparasitarios = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Antiparasitarios");
            var catAlimento = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Alimento y Nutrición");
            var catQuirurgico = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Material Quirúrgico e Insumos");

            if (catFarma != null && catVacunas != null && catAntiparasitarios != null && catAlimento != null && catQuirurgico != null)
            {
                var p1 = new Product
                {
                    Name = "Amoxicilina + Ácido Clavulánico 250mg",
                    Sku = "MED-AMX-250",
                    Description = "Antibiótico de amplio espectro para infecciones respiratorias, dérmicas y urinarias. Caja x 20 comp.",
                    IdCategory = catFarma.IdCategory,
                    MinimumStockThreshold = 10,
                    UnitPrice = 18.50m,
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                };

                var p2 = new Product
                {
                    Name = "Vacuna Puppy DP (Distemper + Parvo)",
                    Sku = "VAC-PUP-DP",
                    Description = "Inmunización activa de cachorros contra Moquillo y Parvovirosis canina. Vial monodosis.",
                    IdCategory = catVacunas.IdCategory,
                    MinimumStockThreshold = 15,
                    UnitPrice = 12.00m,
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                };

                var p3 = new Product
                {
                    Name = "Bravecto Comprimido Masticable (10 a 20 kg)",
                    Sku = "PAR-BRV-1020",
                    Description = "Antiparasitario sistémico contra pulgas y garrapatas con acción prolongada de 12 semanas.",
                    IdCategory = catAntiparasitarios.IdCategory,
                    MinimumStockThreshold = 8,
                    UnitPrice = 36.00m,
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                };

                var p4 = new Product
                {
                    Name = "Meloxicam Gotas Orales 1.5mg/ml 32ml",
                    Sku = "MED-MLX-032",
                    Description = "Antiinflamatorio no esteroideo (AINE) para control del dolor e inflamación musculoesquelética.",
                    IdCategory = catFarma.IdCategory,
                    MinimumStockThreshold = 6,
                    UnitPrice = 14.80m,
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                };

                var p5 = new Product
                {
                    Name = "Royal Canin Gastrointestinal Canino 2kg",
                    Sku = "ALM-RC-GAS2K",
                    Description = "Alimento seco dietético de alta digestibilidad para trastornos gastrointestinales caninos.",
                    IdCategory = catAlimento.IdCategory,
                    MinimumStockThreshold = 5,
                    UnitPrice = 32.50m,
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                };

                var p6 = new Product
                {
                    Name = "Catéter Intravenoso 22G Nipro (Caja x 50)",
                    Sku = "QUI-CAT-22G",
                    Description = "Catéter periférico radiopaco con teflón estéril para fluidoterapia y medicación continua.",
                    IdCategory = catQuirurgico.IdCategory,
                    MinimumStockThreshold = 20,
                    UnitPrice = 15.00m,
                    IsActive = true,
                    CreationDate = DateTime.UtcNow
                };

                await context.Products.AddRangeAsync(p1, p2, p3, p4, p5, p6);
                await context.SaveChangesAsync();

                // 5. Seed Inventario inicial distribuido en las 3 sucursales
                var matriz = await context.Stores.FirstOrDefaultAsync(s => s.Name.Contains("Matriz"));
                var llanoGrande = await context.Stores.FirstOrDefaultAsync(s => s.Name.Contains("Llano Grande"));
                var petShop = await context.Stores.FirstOrDefaultAsync(s => s.Name.Contains("Pet Shop"));

                var initialInventories = new List<ProductInventory>();

                // Inventario Sede Matriz (Clínica 24h)
                if (matriz != null)
                {
                    initialInventories.AddRange(new[]
                    {
                        new ProductInventory
                        {
                            IdStore = matriz.IdStore,
                            IdProduct = p1.IdProduct,
                            BatchNumber = "LOT-2026-AMX01",
                            ExpirationDate = DateTime.UtcNow.AddMonths(14),
                            CurrentStock = 25,
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        },
                        new ProductInventory
                        {
                            IdStore = matriz.IdStore,
                            IdProduct = p2.IdProduct,
                            BatchNumber = "LOT-2026-PUP01",
                            ExpirationDate = DateTime.UtcNow.AddMonths(2), // Próximo a caducar (Alerta FEFO)
                            CurrentStock = 8, // Por debajo del umbral mínimo (15)
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        },
                        new ProductInventory
                        {
                            IdStore = matriz.IdStore,
                            IdProduct = p3.IdProduct,
                            BatchNumber = "LOT-2025-BRV01",
                            ExpirationDate = DateTime.UtcNow.AddMonths(10),
                            CurrentStock = 4, // Por debajo del umbral mínimo (8)
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        },
                        new ProductInventory
                        {
                            IdStore = matriz.IdStore,
                            IdProduct = p6.IdProduct,
                            BatchNumber = "LOT-2027-CAT01",
                            ExpirationDate = DateTime.UtcNow.AddMonths(24),
                            CurrentStock = 35,
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        }
                    });
                }

                // Inventario Sede Llano Grande (Consultorio Primario)
                if (llanoGrande != null)
                {
                    initialInventories.AddRange(new[]
                    {
                        new ProductInventory
                        {
                            IdStore = llanoGrande.IdStore,
                            IdProduct = p1.IdProduct,
                            BatchNumber = "LOT-2026-AMX02",
                            ExpirationDate = DateTime.UtcNow.AddMonths(11),
                            CurrentStock = 12,
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        },
                        new ProductInventory
                        {
                            IdStore = llanoGrande.IdStore,
                            IdProduct = p2.IdProduct,
                            BatchNumber = "LOT-2026-PUP02",
                            ExpirationDate = DateTime.UtcNow.AddMonths(6),
                            CurrentStock = 5, // Bajo umbral
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        },
                        new ProductInventory
                        {
                            IdStore = llanoGrande.IdStore,
                            IdProduct = p4.IdProduct,
                            BatchNumber = "LOT-2026-MLX01",
                            ExpirationDate = DateTime.UtcNow.AddMonths(18),
                            CurrentStock = 8,
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        }
                    });
                }

                // Inventario Sede Pet Shop & Guardería
                if (petShop != null)
                {
                    initialInventories.AddRange(new[]
                    {
                        new ProductInventory
                        {
                            IdStore = petShop.IdStore,
                            IdProduct = p3.IdProduct,
                            BatchNumber = "LOT-2026-BRV02",
                            ExpirationDate = DateTime.UtcNow.AddMonths(12),
                            CurrentStock = 15,
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        },
                        new ProductInventory
                        {
                            IdStore = petShop.IdStore,
                            IdProduct = p5.IdProduct,
                            BatchNumber = "LOT-2026-RC01",
                            ExpirationDate = DateTime.UtcNow.AddMonths(8),
                            CurrentStock = 2, // Crítico (umbral 5)
                            IsActive = true,
                            CreationDate = DateTime.UtcNow
                        }
                    });
                }

                if (initialInventories.Any())
                {
                    await context.ProductInventories.AddRangeAsync(initialInventories);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
