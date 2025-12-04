using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;

public class TaxesService
{
    private readonly TaxesRepository _taxesRepository;

    public TaxesService(TaxesRepository repository)
    {
        _taxesRepository = repository;
    }

    public GenericResponse<Taxes> GetTaxes()
    {
        return _taxesRepository.GetTaxes();
    }

    public GenericResponse<Taxes> UpdateTaxes(Taxes updated)
    {
        return _taxesRepository.UpdateTaxes(updated);
    }
}