using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure;

public static class DbInitializer
{
    public static void Seed(NordikAventureContext context)
    {
        context.Database.EnsureCreated();
        
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Id = 1, Name = "Tentes & abris" },
                new Category { Id = 2, Name = "Sacs & portage" },
                new Category { Id = 3, Name = "Vetements techniques" },
                new Category { Id = 4, Name = "Accessoires & cuisine" },
                new Category { Id = 5, Name = "Electronique & navigation" }
            );
        }
        
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(
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
        }
        
        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee
                {
                    Id = 1, Name = "Marc", Surname = "Leblond", Password = "marc123*", EmailAddress = "marc123@gmail.com",
                    PhoneNumber = "418-882-8636", HireDate = DateTime.Today, Role = "Employee",
                },
                new Employee
                {
                    Id = 2, Name = "Jean", Surname = "Laronde", Password = "jean123*", EmailAddress = "jean123@gmail.com",
                    PhoneNumber = "418-882-8646", HireDate = DateTime.Today, Role = "Manager"
                },
                new Employee
                {
                    Id = 3, Name = "Arjean", Surname = "Labonde", Password = "money123*", EmailAddress = "money@gmail.com",
                    PhoneNumber = "418-182-8646", HireDate = DateTime.Today, Role = "Accountant"
                }
            );
        }
        
        if (!context.Suppliers.Any())
        {
            context.Suppliers.AddRange(
                new Supplier { Id = 1, Name = "AventureX", AverageDeliveryTime = "1 jour", Code = "AX" },
                new Supplier { Id = 2, Name = "TrekSupply", AverageDeliveryTime = "5 jour", Code = "TS" },
                new Supplier { Id = 3, Name = "MontNord", AverageDeliveryTime = "6 jour", Code = "MN" },
                new Supplier { Id = 4, Name = "NordPack", AverageDeliveryTime = "3 jour", Code = "NP" },
                new Supplier { Id = 5, Name = "NordWear", AverageDeliveryTime = "4 jour", Code = "NW" },
                new Supplier { Id = 6, Name = "ArcticLine", AverageDeliveryTime = "2 jour", Code = "AL" },
                new Supplier { Id = 7, Name = "TechTrail", AverageDeliveryTime = "2 jour", Code = "TT" }
            );
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    Id = 1, Sku = "NC-TNT-001",
                    Name = "Tente légère 2 places", PriceToBuy = 145.00, PriceToSell = 299.00, PaybackToSupplier = 0.05,
                    Status = "Actif", Weight = 2.8, SupplierId = 1, CategoryId = 1
                },
                new Product
                {
                    Id = 2, Sku = "NC-TNT-002", Name = "Tente familiale 6 places", PriceToBuy = 260.00,
                    PriceToSell = 499.00, PaybackToSupplier = 0.05,
                    Status = "Actif", Weight = 6.5, SupplierId = 1, CategoryId = 1
                },
                new Product
                {
                    Id = 3, Sku = "NC-TNT-003", Name = "Toile imperméable 3x3 m", PriceToBuy = 25.00,
                    PriceToSell = 59.00, PaybackToSupplier = 0.04,
                    Status = "Actif", Weight = 1.1, SupplierId = 2, CategoryId = 1
                },
                new Product
                {
                    Id = 4, Sku = "NC-TNT-004", Name = "Tapis de sol isolant", PriceToBuy = 18.00, PriceToSell = 39.00,
                    PaybackToSupplier = 0.03,
                    Status = "Actif", Weight = 0.9, SupplierId = 3, CategoryId = 1
                },
                new Product
                {
                    Id = 5, Sku = "NC-TNT-005", Name = "Abri cuisine pliable", PriceToBuy = 75.00, PriceToSell = 149.00,
                    PaybackToSupplier = 0.05,
                    Status = "Actif", Weight = 5, SupplierId = 1, CategoryId = 1
                },
                new Product
                {
                    Id = 6, Sku = "NC-TNT-006", Name = "Mat telescopique alu", PriceToBuy = 12.00, PriceToSell = 29.00,
                    PaybackToSupplier = 0.04,
                    Status = "Actif", Weight = 0.7, SupplierId = 2, CategoryId = 1
                },
                new Product
                {
                    Id = 7, Sku = "NC-SAC-001", Name = "Sac à dos 50 L etanche", PriceToBuy = 65.00,
                    PriceToSell = 139.00,
                    PaybackToSupplier = 0.06, Status = "Actif", Weight = 1.3, SupplierId = 4, CategoryId = 2
                }, new Product
                {
                    Id = 8, Sku = "NC-SAC-002", Name = "Sac de jour 25 L", PriceToBuy = 32.00, PriceToSell = 79.00,
                    PaybackToSupplier = 0.06, Status = "Actif", Weight = 0.9, SupplierId = 4, CategoryId = 2
                },
                new Product
                {
                    Id = 9, Sku = "NC-SAC-003", Name = "Sac de couchage -10 degree", PriceToBuy = 80.00,
                    PriceToSell = 169.00,
                    PaybackToSupplier = 0.03, Status = "Actif", Weight = 2.2, SupplierId = 3, CategoryId = 2
                },
                new Product
                {
                    Id = 10, Sku = "NC-SAC-004", Name = "Tapis autogonflant", PriceToBuy = 25.00, PriceToSell = 59.00,
                    PaybackToSupplier = 0.03, Status = "Actif", Weight = 1.1, SupplierId = 3, CategoryId = 2
                },
                new Product
                {
                    Id = 11, Sku = "NC-SAC-005", Name = "Housse impermeable sac a dos", PriceToBuy = 9.00,
                    PriceToSell = 19.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.4, SupplierId = 2, CategoryId = 2
                },
                new Product
                {
                    Id = 12, Sku = "NC-SAC-006", Name = "Batons de marche carbone", PriceToBuy = 35.00,
                    PriceToSell = 79.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.8, SupplierId = 2, CategoryId = 2
                },
                new Product
                {
                    Id = 13, Sku = "NC-VET-001", Name = "Chandail thermique homme", PriceToBuy = 22.00,
                    PriceToSell = 59.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.6, SupplierId = 5, CategoryId = 3
                },
                new Product
                {
                    Id = 14, Sku = "NC-VET-002", Name = "Chandail thermique femme", PriceToBuy = 22.00,
                    PriceToSell = 59.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.6, SupplierId = 5, CategoryId = 3
                },
                new Product
                {
                    Id = 15, Sku = "NC-VET-003", Name = "Pantalon de randonnee homme", PriceToBuy = 38.00,
                    PriceToSell = 89.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.8, SupplierId = 5, CategoryId = 3
                },
                new Product
                {
                    Id = 16, Sku = "NC-VET-004", Name = "Pantalon de randonnee femme", PriceToBuy = 38.00,
                    PriceToSell = 89.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.8, SupplierId = 5, CategoryId = 3
                },
                new Product
                {
                    Id = 17, Sku = "NC-VET-005", Name = "Manteau coupe-vent", PriceToBuy = 55.00, PriceToSell = 129.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 1.1, SupplierId = 6, CategoryId = 3
                },
                new Product
                {
                    Id = 18, Sku = "NC-VET-006", Name = "Tuque en laine merinos", PriceToBuy = 10.00,
                    PriceToSell = 29.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.3, SupplierId = 6, CategoryId = 3
                },
                new Product
                {
                    Id = 19, Sku = "NC-VET-007", Name = "Gants isolants Hiver+", PriceToBuy = 18.00,
                    PriceToSell = 45.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.5, SupplierId = 6, CategoryId = 3
                },
                new Product
                {
                    Id = 20, Sku = "NC-ACC-001", Name = "Rechaud portatif", PriceToBuy = 25.00, PriceToSell = 59.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.9, SupplierId = 2, CategoryId = 4
                },
                new Product
                {
                    Id = 21, Sku = "NC-ACC-002", Name = "Bouteille isotherme 1L", PriceToBuy = 12.00,
                    PriceToSell = 29.00,
                    PaybackToSupplier = 0.03, Status = "Actif", Weight = 0.4, SupplierId = 3, CategoryId = 4
                },
                new Product
                {
                    Id = 22, Sku = "NC-ACC-003", Name = "Lampe frontale 300 lumens", PriceToBuy = 14.00,
                    PriceToSell = 39.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.2, SupplierId = 1, CategoryId = 4
                },
                new Product
                {
                    Id = 23, Sku = "NC-ACC-004", Name = "Ensemble vaisselle 4 pers.", PriceToBuy = 20.00,
                    PriceToSell = 49.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 1.2, SupplierId = 2, CategoryId = 4
                },
                new Product
                {
                    Id = 24, Sku = "NC-ACC-005", Name = "Filtre a eau compact", PriceToBuy = 28.00, PriceToSell = 69.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.7, SupplierId = 1, CategoryId = 4
                },
                new Product
                {
                    Id = 25, Sku = "NC-ACC-006", Name = "Couteau multifonction", PriceToBuy = 15.00,
                    PriceToSell = 39.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.5, SupplierId = 4, CategoryId = 4
                },
                new Product
                {
                    Id = 26, Sku = "NC-ELE-001", Name = "Montre GPS plein air", PriceToBuy = 120.00,
                    PriceToSell = 279.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.9, SupplierId = 7, CategoryId = 5
                },
                new Product
                {
                    Id = 27, Sku = "NC-ELE-002", Name = "Chargeur solaire 20W", PriceToBuy = 35.00, PriceToSell = 79.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.6, SupplierId = 7, CategoryId = 5
                },
                new Product
                {
                    Id = 28, Sku = "NC-ELE-003", Name = "Boussole de précision", PriceToBuy = 9.00, PriceToSell = 24.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.2, SupplierId = 2, CategoryId = 5
                },
                new Product
                {
                    Id = 29, Sku = "NC-ELE-004", Name = "Radio météo portable", PriceToBuy = 22.00, PriceToSell = 49.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.8, SupplierId = 7, CategoryId = 5
                },
                new Product
                {
                    Id = 30, Sku = "NC-ELE-005", Name = "Lampe USB rechargeable", PriceToBuy = 11.00,
                    PriceToSell = 25.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.3, SupplierId = 7, CategoryId = 5
                }
            );
        }

        context.SaveChanges();
    }
}