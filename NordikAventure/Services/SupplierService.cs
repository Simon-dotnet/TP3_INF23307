using GestBibli.Objects;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class SupplierService
{
    SupplierRepository _supplierRepository;

    public SupplierService(SupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public GenericResponse<List<Supplier>> GetAllSuppliers()
    {
        return _supplierRepository.GetAllSuppliersWithItsProducts();
    }

    public GenericResponse<Supplier> AddSupplier(Supplier supplier)
    {
        return _supplierRepository.AddSupplier(supplier);
    } 
}