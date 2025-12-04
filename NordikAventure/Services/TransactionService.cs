using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;

    public TransactionService(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public GenericResponse<Transaction> AddEnteringTransaction(Transaction transaction)
    {
        return _transactionRepository.AddEnteringTransaction(transaction);
    }
    
    public GenericResponse<Transaction> AddLeavingTransaction(Transaction transaction)
    {
        return _transactionRepository.AddLeavingTransaction(transaction);
    }

    public GenericResponse<double> GetProfitFromWeek()
    {
        return _transactionRepository.GetProfitOfTheWeek();
    }
    
    public GenericResponse<List<Transaction>> GetLastTransactions(int count)
    {
        return _transactionRepository.GetLast(count);
    }

    public GenericResponse<List<Transaction>> GetAllTransactions()
    {
        return _transactionRepository.GetAll();
    }
}