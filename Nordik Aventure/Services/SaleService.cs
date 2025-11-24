using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class SaleService
{
    private SaleRepository _saleRepository;

    public SaleService(SaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public GenericResponse<Sale> GetSaleById(int id)
    {
        return _saleRepository.GetSaleById(id);
    }
}