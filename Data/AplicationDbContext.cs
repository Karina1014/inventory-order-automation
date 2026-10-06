using Microsoft.EntityFrameworkCore; 
using InventoryFlow.Models;
public class AplicationDbContext: DbContext {
                             
    public  AplicationDbContext (DbContextOptions<AplicationDbContext> options): base(options)
    {
        
    } 

    public DbSet <Store> Stores { get; set; }
     public DbSet <StoreType> StoreTypes { get; set; }
}