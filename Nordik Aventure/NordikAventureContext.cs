using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.Models.User;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Nordik_Aventure;

public class NordikAventureContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSupplierProduct> OrderSupplierProducts { get; set; }
    
    public DbSet<Stock> Stock { get; set; }
    
    public DbSet<ProductInStock> ProductInStock { get; set; }
    
    //Modèles Finance
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleDetails> SaleDetails { get; set; }
    public DbSet<SaleReceipt> SaleReceipts { get; set; }
    public DbSet<SupplierReceipt> SupplierReceipts { get; set; }
    public DbSet<Taxes> Taxes { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionHistory> TransactionHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string server = "localhost";
        const string database = "nordikaventure";
        const string user = "root";
        const string password = "admin123*";
        const string connectionString = $"Server={server};Database={database};User={user};Password={password};";
        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString),
            mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>();
        modelBuilder.Entity<Client>();
        modelBuilder.Entity<Employee>();
        modelBuilder.Entity<Product>();
        modelBuilder.Entity<Supplier>();
        modelBuilder.Entity<Order>();
        modelBuilder.Entity<OrderSupplierProduct>();
        modelBuilder.Entity<Stock>();
        modelBuilder.Entity<ProductInStock>();
        
        // Modèles Finance
        modelBuilder.Entity<Payment>();
        modelBuilder.Entity<Purchase>();
        modelBuilder.Entity<PurchaseDetails>();
        modelBuilder.Entity<Sale>();
        modelBuilder.Entity<SaleDetails>();
        modelBuilder.Entity<SaleReceipt>();
        modelBuilder.Entity<SupplierReceipt>();
        modelBuilder.Entity<Taxes>().HasNoKey();
        modelBuilder.Entity<Transaction>();
        modelBuilder.Entity<TransactionHistory>();

    }
}