using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Repositories;

public class MovementHistoryRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public MovementHistoryRepository()
    {
    }

    public GenericResponse<List<MovementHistory>> GetLast30MovementHistory()
    {
        try
        {
            var result = _context.MovementHistory.OrderByDescending(m => m.Date).Take(30).AsQueryable()
                .Include(mh => mh.Employee)
                .Include(mh => mh.Purchase)
                .ThenInclude(p => p.PurchaseDetails)
                .ThenInclude(pd => pd.Product)
                .ThenInclude(p => p.Supplier)
                .Include(mh => mh.Sale)
                .ThenInclude(s => s.SaleDetails)
                .ThenInclude(sd => sd.ProductInStock)
                .ThenInclude(ps => ps.Product)
                .Include(mh => mh.Sale)
                .ThenInclude(s => s.Client)
                .ToList();
            return new GenericResponse<List<MovementHistory>>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<List<MovementHistory>>($"Erreur de get dernier 30 movement history: {ex}", 500);
        }
    }

    public GenericResponse<MovementHistory> AddEnteringMovementStock(MovementHistory movementHistory)
    {
        try
        {
            _context.MovementHistory.Add(movementHistory);
            _context.SaveChanges();
            return new GenericResponse<MovementHistory>(movementHistory);
        }
        catch (Exception ex)
        {
            return new GenericResponse<MovementHistory>(
                $"Erreur lors de la sauvegarde du movement history entrante: {ex}", 500);
        }
    }

    public GenericResponse<MovementHistory> AddLeavingMovementStock(MovementHistory movementHistory)
    {
        try
        {
            _context.MovementHistory.Add(movementHistory);
            _context.SaveChanges();
            return new GenericResponse<MovementHistory>(movementHistory);
        }
        catch (Exception ex)
        {
            return new GenericResponse<MovementHistory>(
                $"Erreur lors de la sauvegarde du movement history sortante: {ex}", 500);
        }
    }
}