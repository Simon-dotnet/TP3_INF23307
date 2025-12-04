using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;

namespace Nordik_Aventure.Repositories;

public class ClientInteractionsRepository
{
    private readonly NordikAventureContext _context;

    public ClientInteractionsRepository(NordikAventureContext context)
    {
        _context = context;
    }

    public GenericResponse<ClientInterraction> GetClientInterractionById(int id)
    {
        try
        {
            var clientInterraction = _context.ClientInterractions.Where(i => i.Id == id)
                .Include(ci => ci.Client)
                .Include(ci => ci.Employee)
                .SingleOrDefault();
            return new GenericResponse<ClientInterraction>(clientInterraction);
        }
        catch (Exception ex)
        {
            return new GenericResponse<ClientInterraction>($"Erreur lors du get d'une interaction client: {ex.Message}",
                500);
        }
    }

    public GenericResponse<List<ClientInterraction>> GetClientInterractionByClientId(int clientId)
    {
        try
        {
            var clientInterractions = _context.ClientInterractions.Where(i => i.ClientId == clientId)
                .Include(ci => ci.Client)
                .Include(ci => ci.Employee)
                .OrderByDescending(ci => ci.Date)
                .ToList();
            return new GenericResponse<List<ClientInterraction>>(clientInterractions);
        }
        catch (Exception e)
        {
            return new GenericResponse<List<ClientInterraction>>(
                "Erreur lors du get des interactions client par client: {ex.Message}", 500);
        }
    }

    public GenericResponse<ClientInterraction> AddClientInteraction(ClientInterraction clientInterraction)
    {
        try
        {
            _context.ClientInterractions.Add(clientInterraction);
            _context.SaveChanges();
            return new GenericResponse<ClientInterraction>(clientInterraction);
        }
        catch (Exception e)
        {
            return new GenericResponse<ClientInterraction>(
                "Erreur lors de l'ajout de l'interaction client: {e.Message}", 500);
        }
    }
}