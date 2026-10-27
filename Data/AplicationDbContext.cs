using Microsoft.EntityFrameworkCore; 
using InventoryFlow.Models;
public class AplicationDbContext: DbContext {
                             
    public  AplicationDbContext (DbContextOptions<AplicationDbContext> options): base(options)
    {
        
    } 

    public DbSet <Store> Stores { get; set; }
    public DbSet <StoreType> StoreTypes { get; set; }
    public DbSet <Product> Products { get; set; }
    public DbSet <Category> Categories { get; set; }
    public DbSet <ProductInventory> ProductInventories { get; set; }
    public DbSet <MovementType> MovementTypes { get; set; }
    public DbSet <InventoryMovement> InventoryMovements { get; set; }
}