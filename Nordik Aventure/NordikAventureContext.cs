using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.User;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Nordik_Aventure;

public class NordikAventureContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }

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
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Tentes & abris"},
            new Category { Id = 2, Name = "Sacs & portage"},
            new Category { Id = 3, Name = "Vetements techniques"},
            new Category { Id = 4, Name = "Accessoires & cuisine"},
            new Category { Id = 5, Name = "Electronique & navigation"}
        );

        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                Id = 1, Name = "Paul", Password = "Paul123", Address = "144 rue de paul, Lévis, Qc, Canada",
                Email = "paul@paul.ca", Phone = "418-878-4090", Type = "particulier"
            },
            new Client
            {
                Id = 2, Name = "KayakManiac", Password = "Kayak123", Address = "123 rue du kayak, Montréal, Qc, Canada",
                Email = "kayak@kayak.ca", Phone = "418-878-4990", Type = "entreprise"
            }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "Marc", Surname = "Leblond", Password = "marc123*", EmailAddress = "marc123@gmail.com", PhoneNumber = "418-882-8636", HireDate = DateTime.Today, Role = "Employee" },
            new Employee { Id = 2, Name = "Jean", Surname = "Laronde", Password = "jean123*", EmailAddress = "jean123@gmail.com", PhoneNumber = "418-882-8646", HireDate = DateTime.Today, Role = "Manager" },
            new Employee { Id = 3, Name = "Arjean", Surname = "Labonde", Password = "money123*", EmailAddress = "money@gmail.com", PhoneNumber = "418-182-8646", HireDate = DateTime.Today, Role = "Accountant" }
        );
    }
}