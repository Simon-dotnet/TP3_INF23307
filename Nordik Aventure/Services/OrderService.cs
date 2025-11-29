using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly TaxesRepository _taxeRepository;


    public OrderService(OrderRepository orderRepository, TaxesRepository taxesRepository)
    {
        _orderRepository = orderRepository;
        _taxeRepository = taxesRepository;
    }

    public GenericResponse<List<Order>> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public GenericResponse<Order> GetOrderById(int id)
    {
        return _orderRepository.GetOrderById(id);
    }

    public GenericResponse<Order> CreateOrder(OrderCreateViewModel createModel, int purchaseId)
    {
        if (createModel == null || createModel.Items == null || !createModel.Items.Any())
            return new GenericResponse<Order>
            {
                Code = 500,
                Data = null,
                Message = "Erreur inattendue",
                Success = false
            };

        var order = new Order
        {
            DateOfOrdering = DateTime.UtcNow,
            DateOfDelivery = createModel.DateOfDelivery ?? DateTime.UtcNow.AddDays(7),
            OrderSupplierProducts = new List<OrderSupplierProduct>(),
            PurchaseId = purchaseId,
        };

        double total = 0.0;

        foreach (var item in createModel.Items)
        {
            var totalPurchase = item.TotalPrice;
            var tvq = totalPurchase * (_taxeRepository.GetTaxes().Data.ValueTvq/100);
            var tps = totalPurchase * (_taxeRepository.GetTaxes().Data.ValueTps/100);
            var totalWithTaxes = tvq + tps + totalPurchase;
            var osp = new OrderSupplierProduct
            {
                ProductId = item.ProductId,
                SupplierId = item.SupplierId,
                Quantity = item.Quantity,
                TotalPrice = totalWithTaxes,
            };

            order.OrderSupplierProducts.Add(osp);
            total += totalWithTaxes;
        }

        order.TotalPrice = total;

        return _orderRepository.CreateOrder(order);
    }
    
    public GenericResponse<Order> ChangeOrderStatus(int orderId, string newStatus)
    {
        var allowed = new[] { "réception", "préparation", "expédiée", "facturée", "payée/fermée" };

        if (!allowed.Contains(newStatus))
            return new GenericResponse<Order>("Statut invalide", 400);

        return _orderRepository.UpdateOrderStatus(orderId, newStatus);
    }
    
    public GenericResponse<bool> DeleteOrder(int id)
    {
        return _orderRepository.DeleteOrder(id);
    }

    public GenericResponse<List<OrderSupplierProduct>> GetOrderItems(int orderId)
    {
        var result = _orderRepository.GetOrderById(orderId);
        if (!result.Success || result.Data == null)
            return new GenericResponse<List<OrderSupplierProduct>>("Commande introuvable", 404);

        return new GenericResponse<List<OrderSupplierProduct>>(result.Data.OrderSupplierProducts.ToList());
    }

}