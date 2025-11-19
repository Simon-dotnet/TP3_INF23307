using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

public class TaxesRepository
{
    private readonly NordikAventureContext _context;

    public TaxesRepository(NordikAventureContext context)
    {
        _context = context;
    }

    public GenericResponse<Taxes> GetTaxes()
    {
        var result = _context.Taxes.FirstOrDefault();
        if (result == null)
        {
            return new GenericResponse<Taxes>("Erreur lors du chargement des taxes", 500);
        }

        return new GenericResponse<Taxes>(result);
    }

    public GenericResponse<Taxes> UpdateTaxes(Taxes newValues)
    {
        try
        {
            var existing = _context.Taxes.FirstOrDefault(t => t.TaxesId == newValues.TaxesId);
            if (existing == null)
                return new GenericResponse<Taxes>("Taxes record not found", 404);
            
            existing.ValueTps = newValues.ValueTps;
            existing.ValueTvq = newValues.ValueTvq;

            _context.SaveChanges();
            return new GenericResponse<Taxes>(existing);
        }
        catch (Exception e)
        {
            return new GenericResponse<Taxes>($"error:{e}", 500);
        }
    }
}