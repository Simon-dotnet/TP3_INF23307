using Nordik_Aventure;
using Nordik_Aventure.Controllers;
using Nordik_Aventure.Mapper;
using Nordik_Aventure.Repositories;
using Nordik_Aventure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NordikAventureContext>();

builder.Services.AddScoped<StockMovementController>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<SupplierRepository>();

builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<StockRepository>();

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddScoped<TaxesService>();
builder.Services.AddScoped<TaxesRepository>();

builder.Services.AddScoped<MovementHistoryService>();
builder.Services.AddScoped<MovementHistoryRepository>();

builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<TransactionRepository>();

builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<PurchaseRepository>();

builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<SaleRepository>();

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ClientRepository>();

builder.Services.AddScoped<UserSession>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(typeof(Nordik_Aventure.Mapper.Mapper));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NordikAventureContext>();
    DbInitializer.Seed(db);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();

app.UseRouting();
app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
        name: "default",
        pattern: "/Login")
    .WithStaticAssets();

app.Run();