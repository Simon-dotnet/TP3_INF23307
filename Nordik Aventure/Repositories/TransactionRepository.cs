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
}