using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Nordik_Aventure.Objects.Models;
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
    
    public GenericResponse<Sale> AddSale(Sale sale)
    {
        return _saleRepository.AddSale(sale);
    }
}