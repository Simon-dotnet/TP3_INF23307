using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class TransactionRepository
{
    private readonly NordikAventureContext _context;

    public TransactionRepository(NordikAventureContext context)
    {
        _context = context;
    }

    public GenericResponse<Transaction> AddEnteringTransaction(Transaction transaction)
    {
        try
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return new GenericResponse<Transaction>(transaction);
        }
        catch (Exception e)
        {
            return new GenericResponse<Transaction>($"Erreur lors de la création de la transaction entrante: {e}", 500);
        }
    }

    public GenericResponse<Transaction> AddLeavingTransaction(Transaction transaction)
    {
        try
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return new GenericResponse<Transaction>(transaction);
        }
        catch (Exception e)
        {
            return new GenericResponse<Transaction>($"Erreur lors de la création de la transaction sortante: {e}", 500);
        }
    }

    public GenericResponse<double> GetProfitOfTheWeek()
    {
        try
        {
            var sales = _context.Transactions.Where(t => t.Type == "sale").Where(t => t.Date >= DateTime.Now.AddDays(-7))
                .ToList();
            var saleTotal = sales.Sum(t => t.AmountTotal);

            var purchases = _context.Transactions.Where(t => t.Type == "purchase")
                .Where(t => t.Date >= DateTime.Now.AddDays(-7)).ToList();
            var purchaseTotal = purchases.Sum(t => t.AmountTotal);
        
            var total = saleTotal - purchaseTotal;
            return new GenericResponse<double>(total);
        }
        catch (Exception e)
        {
            return new GenericResponse<double>("Erreur lors du calcul du profit", 500);
        }
    }
}