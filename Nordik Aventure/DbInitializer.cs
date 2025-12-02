using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
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
            context.SaveChanges();
        }

        if (!context.Clients.Any())
        {
            context.Clients.AddRange(
                new Client
                {
                    Id = 1, Name = "Paul", Password = "Paul123", Address = "144 rue de paul, Lévis, Qc, Canada",
                    Email = "paul@paul.ca", Phone = "418-878-4090", Type = "particulier", Status = "Actif",
                    SatisfactionLevel = 1
                },
                new Client
                {
                    Id = 2, Name = "KayakManiac", Password = "Kayak123",
                    Address = "123 rue du kayak, Montréal, Qc, Canada",
                    Email = "kayak@kayak.ca", Phone = "418-878-4990", Type = "entreprise", Status = "Inactif"
                }
            );
            context.SaveChanges();
        }

        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee
                {
                    Id = 1, Name = "Marc", Surname = "Leblond", Password = "marc123*",
                    EmailAddress = "marc123@gmail.com",
                    PhoneNumber = "418-882-8636", HireDate = DateTime.Today, Role = "Employee",
                },
                new Employee
                {
                    Id = 2, Name = "Jean", Surname = "Laronde", Password = "jean123*",
                    EmailAddress = "jean123@gmail.com",
                    PhoneNumber = "418-882-8646", HireDate = DateTime.Today, Role = "Manager"
                },
                new Employee
                {
                    Id = 3, Name = "Arjean", Surname = "Labonde", Password = "money123*",
                    EmailAddress = "money@gmail.com",
                    PhoneNumber = "418-182-8646", HireDate = DateTime.Today, Role = "Accountant"
                }
            );
            context.SaveChanges();
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
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    Id = 1, Sku = "NC-TNT-001", Name = "Tente légère 2 places", PriceToBuy = 145.00,
                    PriceToSell = 299.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 2.8, SupplierId = 1,
                    CategoryId = 1, GrossMargin = 51.50, Description = "Je suisn une tente legere 2 place"
                },
                new Product
                {
                    Id = 2, Sku = "NC-TNT-002", Name = "Tente familiale 6 places", PriceToBuy = 260.00,
                    PriceToSell = 499.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 6.5, SupplierId = 1,
                    CategoryId = 1, GrossMargin = 47.90, Description = "je suis une tente familiale 6 places"
                },
                new Product
                {
                    Id = 3, Sku = "NC-TNT-003", Name = "Toile imperméable 3x3 m", PriceToBuy = 25.00,
                    PriceToSell = 59.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 1.1, SupplierId = 2,
                    CategoryId = 1, GrossMargin = 57.60, Description = "je suis une toile impermeable"
                },
                new Product
                {
                    Id = 4, Sku = "NC-TNT-004", Name = "Tapis de sol isolant", PriceToBuy = 18.00, PriceToSell = 39.00,
                    PaybackToSupplier = 0.03, Status = "Actif", Weight = 0.9, SupplierId = 3, CategoryId = 1,
                    GrossMargin = 53.80, Description = "je suis un tapis de sol isloant"
                },
                new Product
                {
                    Id = 5, Sku = "NC-TNT-005", Name = "Abri cuisine pliable", PriceToBuy = 75.00, PriceToSell = 149.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 5, SupplierId = 1, CategoryId = 1,
                    GrossMargin = 49.70, Description = "je suis un abri cuisine pliable"
                },
                new Product
                {
                    Id = 6, Sku = "NC-TNT-006", Name = "Mat telescopique alu", PriceToBuy = 12.00, PriceToSell = 29.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.7, SupplierId = 2, CategoryId = 1,
                    GrossMargin = 58.60, Description = "je suis un mat telescopique alu"
                },
                new Product
                {
                    Id = 7, Sku = "NC-SAC-001", Name = "Sac à dos 50 L etanche", PriceToBuy = 65.00,
                    PriceToSell = 139.00, PaybackToSupplier = 0.06, Status = "Actif", Weight = 1.3, SupplierId = 4,
                    CategoryId = 2, GrossMargin = 53.20, Description = "je suis un sac dos 50 L etanche"
                },
                new Product
                {
                    Id = 8, Sku = "NC-SAC-002", Name = "Sac de jour 25 L", PriceToBuy = 32.00, PriceToSell = 79.00,
                    PaybackToSupplier = 0.06, Status = "Actif", Weight = 0.9, SupplierId = 4, CategoryId = 2,
                    GrossMargin = 59.50, Description = "je suis un sac de jour 25 L"
                },
                new Product
                {
                    Id = 9, Sku = "NC-SAC-003", Name = "Sac de couchage -10 degree", PriceToBuy = 80.00,
                    PriceToSell = 169.00, PaybackToSupplier = 0.03, Status = "Actif", Weight = 2.2, SupplierId = 3,
                    CategoryId = 2, GrossMargin = 52.70, Description = "je suis un sac de couchage -10 degree"
                },
                new Product
                {
                    Id = 10, Sku = "NC-SAC-004", Name = "Tapis autogonflant", PriceToBuy = 25.00, PriceToSell = 59.00,
                    PaybackToSupplier = 0.03, Status = "Actif", Weight = 1.1, SupplierId = 3, CategoryId = 2,
                    GrossMargin = 57.60, Description = "je suis un tapis autogonflant"
                },
                new Product
                {
                    Id = 11, Sku = "NC-SAC-005", Name = "Housse impermeable sac a dos", PriceToBuy = 9.00,
                    PriceToSell = 19.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.4, SupplierId = 2,
                    CategoryId = 2, GrossMargin = 52.60, Description = "je suis une housse impermeable sac a dos"
                },
                new Product
                {
                    Id = 12, Sku = "NC-SAC-006", Name = "Batons de marche carbone", PriceToBuy = 35.00,
                    PriceToSell = 79.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.8, SupplierId = 2,
                    CategoryId = 2, GrossMargin = 55.70, Description = "je suis un baton de marche carbonne"
                },
                new Product
                {
                    Id = 13, Sku = "NC-VET-001", Name = "Chandail thermique homme", PriceToBuy = 22.00,
                    PriceToSell = 59.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.6, SupplierId = 5,
                    CategoryId = 3, GrossMargin = 62.70, Description = "je suis un chandail thermique homme"
                },
                new Product
                {
                    Id = 14, Sku = "NC-VET-002", Name = "Chandail thermique femme", PriceToBuy = 22.00,
                    PriceToSell = 59.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.6, SupplierId = 5,
                    CategoryId = 3, GrossMargin = 62.70, Description = "je suis un chandail thermique femme"
                },
                new Product
                {
                    Id = 15, Sku = "NC-VET-003", Name = "Pantalon de randonnee homme", PriceToBuy = 38.00,
                    PriceToSell = 89.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.8, SupplierId = 5,
                    CategoryId = 3, GrossMargin = 57.30, Description = "je suis un pantalon de randonnee homme"
                },
                new Product
                {
                    Id = 16, Sku = "NC-VET-004", Name = "Pantalon de randonnee femme", PriceToBuy = 38.00,
                    PriceToSell = 89.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.8, SupplierId = 5,
                    CategoryId = 3, GrossMargin = 57.30, Description = "je suis un pantalon de randonnee femme"
                },
                new Product
                {
                    Id = 17, Sku = "NC-VET-005", Name = "Manteau coupe-vent", PriceToBuy = 55.00, PriceToSell = 129.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 1.1, SupplierId = 6, CategoryId = 3,
                    GrossMargin = 57.40, Description = "je suis un manteau coupe-vent"
                },
                new Product
                {
                    Id = 18, Sku = "NC-VET-006", Name = "Tuque en laine merinos", PriceToBuy = 10.00,
                    PriceToSell = 29.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.3, SupplierId = 6,
                    CategoryId = 3, GrossMargin = 65.50, Description = "je suis un tuque en laine merinos"
                },
                new Product
                {
                    Id = 19, Sku = "NC-VET-007", Name = "Gants isolants Hiver+", PriceToBuy = 18.00,
                    PriceToSell = 45.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.5, SupplierId = 6,
                    CategoryId = 3, GrossMargin = 60.00, Description = "je suis un gants isolants Hiver+"
                },
                new Product
                {
                    Id = 20, Sku = "NC-ACC-001", Name = "Rechaud portatif", PriceToBuy = 25.00, PriceToSell = 59.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.9, SupplierId = 2, CategoryId = 4,
                    GrossMargin = 57.60, Description = "je suis un rechaud portatif"
                },
                new Product
                {
                    Id = 21, Sku = "NC-ACC-002", Name = "Bouteille isotherme 1L", PriceToBuy = 12.00,
                    PriceToSell = 29.00, PaybackToSupplier = 0.03, Status = "Actif", Weight = 0.4, SupplierId = 3,
                    CategoryId = 4, GrossMargin = 58.60, Description = "je suis une bouteille isotherme"
                },
                new Product
                {
                    Id = 22, Sku = "NC-ACC-003", Name = "Lampe frontale 300 lumens", PriceToBuy = 14.00,
                    PriceToSell = 39.00, PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.2, SupplierId = 1,
                    CategoryId = 4, GrossMargin = 64, Description = "je suis un lampe frontale"
                },
                new Product
                {
                    Id = 23, Sku = "NC-ACC-004", Name = "Ensemble vaisselle 4 pers.", PriceToBuy = 20.00,
                    PriceToSell = 49.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 1.2, SupplierId = 2,
                    CategoryId = 4, GrossMargin = 59.20, Description = "je suis un ensemble vaisselle"
                },
                new Product
                {
                    Id = 24, Sku = "NC-ACC-005", Name = "Filtre a eau compact", PriceToBuy = 28.00, PriceToSell = 69.00,
                    PaybackToSupplier = 0.05, Status = "Actif", Weight = 0.7, SupplierId = 1, CategoryId = 4,
                    GrossMargin = 59.40, Description = "je suis un filtre a eau compact"
                },
                new Product
                {
                    Id = 25, Sku = "NC-ACC-006", Name = "Couteau multifonction", PriceToBuy = 15.00,
                    PriceToSell = 39.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.5, SupplierId = 4,
                    CategoryId = 4, GrossMargin = 61.50, Description = "je suis un couteau multifonction"
                },
                new Product
                {
                    Id = 26, Sku = "NC-ELE-001", Name = "Montre GPS plein air", PriceToBuy = 120.00,
                    PriceToSell = 279.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.9, SupplierId = 7,
                    CategoryId = 5, GrossMargin = 56.90, Description = "je suis un montre GPS plein air"
                },
                new Product
                {
                    Id = 27, Sku = "NC-ELE-002", Name = "Chargeur solaire 20W", PriceToBuy = 35.00, PriceToSell = 79.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.6, SupplierId = 7, CategoryId = 5,
                    GrossMargin = 55.70, Description = "je suis un chargur solaire 20W"
                },
                new Product
                {
                    Id = 28, Sku = "NC-ELE-003", Name = "Boussole de précision", PriceToBuy = 9.00, PriceToSell = 24.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.2, SupplierId = 2, CategoryId = 5,
                    GrossMargin = 62.50, Description = "je suis une boussole de précision"
                },
                new Product
                {
                    Id = 29, Sku = "NC-ELE-004", Name = "Radio météo portable", PriceToBuy = 22.00, PriceToSell = 49.00,
                    PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.8, SupplierId = 7, CategoryId = 5,
                    GrossMargin = 55.10, Description = "je suis une radio meteo portable"
                },
                new Product
                {
                    Id = 30, Sku = "NC-ELE-005", Name = "Lampe USB rechargeable", PriceToBuy = 11.00,
                    PriceToSell = 25.00, PaybackToSupplier = 0.04, Status = "Actif", Weight = 0.3, SupplierId = 7,
                    CategoryId = 5, GrossMargin = 56.00, Description = "je suis un lampe USB rechargeable"
                }
            );
            context.SaveChanges();
        }

        if (!context.Stock.Any())
        {
            context.Stock.Add(new Stock());
            context.SaveChanges();
        }

        if (!context.ProductInStock.Any())
        {
            context.ProductInStock.AddRange(
                new ProductInStock
                {
                    Id = 1, ProductId = 1, QuantityInStock = 18, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "A1", LastRefill = new DateTime(2025, 3, 2), StockId = 1
                },
                new ProductInStock
                {
                    Id = 2, ProductId = 2, QuantityInStock = 9, Threshold = 3, MinimalQuantity = 2, Status = "Actif",
                    StorageLocation = "A1", LastRefill = new DateTime(2025, 2, 18), StockId = 1
                },
                new ProductInStock
                {
                    Id = 3, ProductId = 3, QuantityInStock = 25, Threshold = 8, MinimalQuantity = 5, Status = "Actif",
                    StorageLocation = "A2", LastRefill = new DateTime(2025, 3, 10), StockId = 1
                },
                new ProductInStock
                {
                    Id = 4, ProductId = 4, QuantityInStock = 40, Threshold = 10, MinimalQuantity = 6, Status = "Actif",
                    StorageLocation = "A2", LastRefill = new DateTime(2025, 3, 5), StockId = 1
                },
                new ProductInStock
                {
                    Id = 5, ProductId = 5, QuantityInStock = 12, Threshold = 4, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "A1", LastRefill = new DateTime(2025, 2, 20), StockId = 1
                },
                new ProductInStock
                {
                    Id = 6, ProductId = 6, QuantityInStock = 30, Threshold = 10, MinimalQuantity = 6, Status = "Actif",
                    StorageLocation = "A3", LastRefill = new DateTime(2025, 3, 8), StockId = 1
                },
                new ProductInStock
                {
                    Id = 7, ProductId = 7, QuantityInStock = 20, Threshold = 6, MinimalQuantity = 4, Status = "Actif",
                    StorageLocation = "B1", LastRefill = new DateTime(2025, 3, 12), StockId = 1
                },
                new ProductInStock
                {
                    Id = 8, ProductId = 8, QuantityInStock = 25, Threshold = 8, MinimalQuantity = 5, Status = "Actif",
                    StorageLocation = "B2", LastRefill = new DateTime(2025, 3, 10), StockId = 1
                },
                new ProductInStock
                {
                    Id = 9, ProductId = 9, QuantityInStock = 15, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "B3", LastRefill = new DateTime(2025, 2, 25), StockId = 1
                },
                new ProductInStock
                {
                    Id = 10, ProductId = 10, QuantityInStock = 35, Threshold = 10, MinimalQuantity = 5,
                    Status = "Actif", StorageLocation = "B3", LastRefill = new DateTime(2025, 3, 5), StockId = 1
                },
                new ProductInStock
                {
                    Id = 11, ProductId = 11, QuantityInStock = 40, Threshold = 10, MinimalQuantity = 6,
                    Status = "Actif", StorageLocation = "B2", LastRefill = new DateTime(2025, 3, 11), StockId = 1
                },
                new ProductInStock
                {
                    Id = 12, ProductId = 12, QuantityInStock = 18, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "B1", LastRefill = new DateTime(2025, 2, 28), StockId = 1
                },
                new ProductInStock
                {
                    Id = 13, ProductId = 13, QuantityInStock = 50, Threshold = 15, MinimalQuantity = 10,
                    Status = "Actif", StorageLocation = "C1", LastRefill = new DateTime(2025, 3, 9), StockId = 1
                },
                new ProductInStock
                {
                    Id = 14, ProductId = 14, QuantityInStock = 48, Threshold = 15, MinimalQuantity = 10,
                    Status = "Actif", StorageLocation = "C1", LastRefill = new DateTime(2025, 3, 9), StockId = 1
                },
                new ProductInStock
                {
                    Id = 15, ProductId = 15, QuantityInStock = 30, Threshold = 8, MinimalQuantity = 4, Status = "Actif",
                    StorageLocation = "C2", LastRefill = new DateTime(2025, 3, 3), StockId = 1
                },
                new ProductInStock
                {
                    Id = 16, ProductId = 16, QuantityInStock = 32, Threshold = 8, MinimalQuantity = 4, Status = "Actif",
                    StorageLocation = "C2", LastRefill = new DateTime(2025, 3, 3), StockId = 1
                },
                new ProductInStock
                {
                    Id = 17, ProductId = 17, QuantityInStock = 20, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "C3", LastRefill = new DateTime(2025, 2, 19), StockId = 1
                },
                new ProductInStock
                {
                    Id = 18, ProductId = 18, QuantityInStock = 40, Threshold = 10, MinimalQuantity = 6,
                    Status = "Actif", StorageLocation = "C4", LastRefill = new DateTime(2025, 3, 10), StockId = 1
                },
                new ProductInStock
                {
                    Id = 19, ProductId = 19, QuantityInStock = 25, Threshold = 8, MinimalQuantity = 4, Status = "Actif",
                    StorageLocation = "C4", LastRefill = new DateTime(2025, 2, 22), StockId = 1
                },
                new ProductInStock
                {
                    Id = 20, ProductId = 20, QuantityInStock = 20, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "D1", LastRefill = new DateTime(2025, 2, 28), StockId = 1
                },
                new ProductInStock
                {
                    Id = 21, ProductId = 21, QuantityInStock = 40, Threshold = 12, MinimalQuantity = 8,
                    Status = "Actif", StorageLocation = "D2", LastRefill = new DateTime(2025, 3, 10), StockId = 1
                },
                new ProductInStock
                {
                    Id = 22, ProductId = 22, QuantityInStock = 35, Threshold = 10, MinimalQuantity = 6,
                    Status = "Actif", StorageLocation = "D3", LastRefill = new DateTime(2025, 3, 12), StockId = 1
                },
                new ProductInStock
                {
                    Id = 23, ProductId = 23, QuantityInStock = 25, Threshold = 8, MinimalQuantity = 5, Status = "Actif",
                    StorageLocation = "D2", LastRefill = new DateTime(2025, 3, 6), StockId = 1
                },
                new ProductInStock
                {
                    Id = 24, ProductId = 24, QuantityInStock = 18, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "D3", LastRefill = new DateTime(2025, 2, 25), StockId = 1
                },
                new ProductInStock
                {
                    Id = 25, ProductId = 25, QuantityInStock = 28, Threshold = 10, MinimalQuantity = 6,
                    Status = "Actif", StorageLocation = "D4", LastRefill = new DateTime(2025, 3, 9), StockId = 1
                },
                new ProductInStock
                {
                    Id = 26, ProductId = 26, QuantityInStock = 10, Threshold = 3, MinimalQuantity = 2, Status = "Actif",
                    StorageLocation = "E1", LastRefill = new DateTime(2025, 2, 17), StockId = 1
                },
                new ProductInStock
                {
                    Id = 27, ProductId = 27, QuantityInStock = 18, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "E2", LastRefill = new DateTime(2025, 3, 1), StockId = 1
                },
                new ProductInStock
                {
                    Id = 28, ProductId = 28, QuantityInStock = 40, Threshold = 12, MinimalQuantity = 8,
                    Status = "Actif", StorageLocation = "E3", LastRefill = new DateTime(2025, 3, 11), StockId = 1
                },
                new ProductInStock
                {
                    Id = 29, ProductId = 29, QuantityInStock = 15, Threshold = 5, MinimalQuantity = 3, Status = "Actif",
                    StorageLocation = "E4", LastRefill = new DateTime(2025, 2, 28), StockId = 1
                },
                new ProductInStock
                {
                    Id = 30, ProductId = 30, QuantityInStock = 35, Threshold = 10, MinimalQuantity = 6,
                    Status = "Actif", StorageLocation = "E5", LastRefill = new DateTime(2025, 3, 7), StockId = 1
                }
            );
            context.SaveChanges();
        }

        if (!context.Taxes.Any())
        {
            context.Taxes.AddRange(new Taxes
            {
                TaxesId = 1,
                ValueTps = 5,
                ValueTvq = 9.975
            });
        }

        if (!context.Transactions.Any() && !context.Purchases.Any() && !context.Sales.Any())
        {
            var client1 = context.Clients.First(c => c.Id == 1);
            var client2 = context.Clients.First(c => c.Id == 2);

            var ps1 = context.ProductInStock.First(p => p.Id == 1);
            var ps2 = context.ProductInStock.First(p => p.Id == 7);
            var ps3 = context.ProductInStock.First(p => p.Id == 13);
            var ps4 = context.ProductInStock.First(p => p.Id == 20);

            var p1 = context.Products.First(p => p.Id == 1);
            var p2 = context.Products.First(p => p.Id == 2);
            var p3 = context.Products.First(p => p.Id == 3);
            var p4 = context.Products.First(p => p.Id == 4);

            var orderData = new[]
            {
                new { Date = new DateTime(2025, 12, 25), P1 = 3, P3 = 2, Status = "réception" },
                new { Date = new DateTime(2025, 12, 20), P1 = 5, P3 = 2, Status = "préparation" },
                new { Date = new DateTime(2025, 11, 29), P1 = 4, P3 = 3, Status = "expédiée" },
                new { Date = new DateTime(2025, 11, 25), P1 = 3, P3 = 4, Status = "facturée" },
                new { Date = new DateTime(2025, 11, 15), P1 = 2, P3 = 3, Status = "payée/fermée" },
                new { Date = new DateTime(2025, 11, 20), P1 = 3, P3 = 3, Status = "réception" },
                new { Date = new DateTime(2025, 11, 10), P1 = 1, P3 = 5, Status = "préparation" },
                new { Date = new DateTime(2025, 11, 5), P1 = 2, P3 = 1, Status = "payée/fermée" }
            };

            int orderIndex = 0;

            foreach (var od in orderData)
            {
                double subtotal =
                    (od.P1 * p1.PriceToBuy) +
                    (od.P3 * p3.PriceToBuy);

                double tps = Math.Round(subtotal * 0.05, 2);
                double tvq = Math.Round(subtotal * 0.09975, 2);
                double total = Math.Round(subtotal + tps + tvq, 2);

                var t = new Transaction
                {
                    Type = "purchase",
                    Date = od.Date,
                    Amount = subtotal,
                    AmountTps = tps,
                    AmountTvq = tvq,
                    AmountTotal = total
                };
                context.Transactions.Add(t);

                var purchase = new Purchase
                {
                    Transaction = t,
                    PurchaseDetails = new List<PurchaseDetails>
                    {
                        new PurchaseDetails { Product = p1, Quantity = od.P1 },
                        new PurchaseDetails { Product = p3, Quantity = od.P3 }
                    }
                };
                context.Purchases.Add(purchase);

                var order = new Order
                {
                    DateOfOrdering = od.Date.AddDays(-3),
                    DateOfDelivery = od.Date,
                    TotalPrice = total,
                    Status = od.Status,
                    Purchase = purchase,
                    OrderSupplierProducts = new List<OrderSupplierProduct>
                    {
                        new OrderSupplierProduct
                        {
                            Product = p1,
                            Supplier = p1.Supplier,
                            Quantity = od.P1,
                            TotalPrice = od.P1 * p1.PriceToBuy
                        },
                        new OrderSupplierProduct
                        {
                            Product = p3,
                            Supplier = p3.Supplier,
                            Quantity = od.P3,
                            TotalPrice = od.P3 * p3.PriceToBuy
                        }
                    }
                };
                context.Orders.Add(order);

                Payment payment;

                switch (orderIndex % 4)
                {
                    case 0:
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Type = "purchase",
                            Status = "payée",
                            RemainingBalance = total
                        };
                        break;

                    case 1:
                        double part = Math.Round(total * 0.45, 2);
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Type = "purchase",
                            Status = "partielle",
                            RemainingBalance = part
                        };
                        break;

                    case 2:
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Type = "purchase",
                            Status = "en attente",
                            RemainingBalance = 0
                        };
                        break;

                    default:
                        double near = Math.Round(total * 0.9, 2);
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Type = "purchase",
                            Status = "partielle",
                            RemainingBalance = near
                        };
                        break;
                }

                context.Payments.Add(payment);

                var sr = new SupplierReceipt
                {
                    Purchase = purchase,
                    Payment = payment,
                    Status = "created"
                };
                context.SupplierReceipts.Add(sr);

                orderIndex++;
            }

            var saleData = new[]
            {
                new { Date = new DateTime(2025, 11, 3), Items = new[] { (ps1, 299), (ps2, 139), (ps3, 169) } },
                new { Date = new DateTime(2025, 11, 6), Items = new[] { (ps1, 299), (ps4, 59) } },
                new { Date = new DateTime(2025, 11, 9), Items = new[] { (ps2, 139), (ps3, 169), (ps4, 79) } },
                new { Date = new DateTime(2025, 11, 10), Items = new[] { (ps1, 299), (ps2, 139) } },
                new { Date = new DateTime(2025, 11, 12), Items = new[] { (ps3, 169), (ps4, 79), (ps1, 299) } },
                new { Date = new DateTime(2025, 11, 14), Items = new[] { (ps3, 169), (ps2, 139) } },
                new { Date = new DateTime(2025, 11, 18), Items = new[] { (ps1, 299), (ps3, 169) } },
                new { Date = new DateTime(2025, 11, 20), Items = new[] { (ps2, 139), (ps3, 169), (ps4, 59) } },
                new { Date = new DateTime(2025, 11, 22), Items = new[] { (ps1, 299), (ps1, 299), (ps2, 139) } },
                new { Date = new DateTime(2025, 11, 24), Items = new[] { (ps1, 299), (ps2, 139), (ps3, 169) } },

                new { Date = new DateTime(2025, 12, 2), Items = new[] { (ps1, 299), (ps4, 59) } },
                new { Date = new DateTime(2025, 12, 4), Items = new[] { (ps2, 139), (ps3, 169) } },
                new { Date = new DateTime(2025, 12, 6), Items = new[] { (ps1, 299), (ps2, 139), (ps4, 59) } },
                new { Date = new DateTime(2025, 12, 9), Items = new[] { (ps3, 169), (ps4, 79) } },
                new { Date = new DateTime(2025, 12, 11), Items = new[] { (ps1, 299), (ps3, 169), (ps4, 59) } },

                new { Date = new DateTime(2025, 11, 26), Items = new[] { (ps1, 299), (ps2, 139) } },
                new { Date = new DateTime(2025, 11, 27), Items = new[] { (ps3, 169) } },
                new { Date = new DateTime(2025, 11, 28), Items = new[] { (ps1, 299), (ps4, 59) } },
                new { Date = new DateTime(2025, 11, 29), Items = new[] { (ps2, 139), (ps4, 59) } },
                new { Date = new DateTime(2025, 11, 30), Items = new[] { (ps3, 169), (ps2, 139) } }
            };

            int saleIndex = 0;

            foreach (var sd in saleData)
            {
                var amount = sd.Items.Sum(x => x.Item2);

                double tps = Math.Round(amount * 0.05, 2);
                double tvq = Math.Round(amount * 0.09975, 2);
                double total = Math.Round(amount + tps + tvq, 2);

                var t = new Transaction
                {
                    Type = "sale",
                    Date = sd.Date,
                    Amount = amount,
                    AmountTps = tps,
                    AmountTvq = tvq,
                    AmountTotal = total
                };
                context.Transactions.Add(t);

                var sale = new Sale
                {
                    Client = sd.Date.Day % 2 == 0 ? client1 : client2,
                    Transaction = t,
                    DateOfSale = sd.Date,
                    TotalPrice = total,
                    SaleDetails = sd.Items.Select(i => new SaleDetails
                    {
                        ProductInStock = i.Item1,
                        Quantity = 1,
                        TotalPrice = i.Item2
                    }).ToList()
                };
                context.Sales.Add(sale);

                Payment payment;

                switch (saleIndex % 3)
                {
                    case 0:
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Status = "payée",
                            RemainingBalance = total,
                            Type = "sale"
                        };
                        break;

                    case 1:
                        double partial = Math.Round(total * 0.4, 2);
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Status = "partielle",
                            RemainingBalance = partial,
                            Type = "sale"
                        };
                        break;

                    default:
                        payment = new Payment
                        {
                            Transaction = t,
                            Amount = total,
                            Status = "en attente",
                            RemainingBalance = 0,
                            Type = "sale"
                        };
                        break;
                }

                context.Payments.Add(payment);

                var r = new SaleReceipt
                {
                    Sale = sale,
                    Payment = payment,
                    Status = "generated"
                };
                context.SaleReceipts.Add(r);

                saleIndex++;
            }

            context.SaveChanges();
        }
    }
}