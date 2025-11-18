using GestBibli.Objects;
using GestBibli.Objects.ViewModels;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class OrderService
{
    OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public GenericResponse<Order> GetProductById(int id)
    {
        return _orderRepository.GetOrderById(id);
    }

    public GenericResponse<List<Order>> GetAllProducts()
    {
        var result = _orderRepository.GetAllOrders();
        return result;
    }
    
    public GenericResponse<Order> CreateOrder(OrderCreateViewModel createModel)
    {
        if (createModel == null || createModel.Items == null || !createModel.Items.Any())
            return new GenericResponse<Order>()
            {
                Code = 500,
                Data = null,
                Message = $"Erreur inattendue",
                Success = false
            };
        
        var order = new Order
        {
            DateOfOrdering = DateTime.UtcNow,
            DateOfDelivery = createModel.DateOfDelivery ?? DateTime.UtcNow.AddDays(7),
            OrderSupplierProducts = new List<OrderSupplierProduct>()
        };

        double total = 0.0;

        foreach (var item in createModel.Items)
        {
            var osp = new OrderSupplierProduct
            {
                ProductId = item.ProductId,
                SupplierId = item.SupplierId,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice,
            };

            order.OrderSupplierProducts.Add(osp);
            total += item.TotalPrice;
        }
        
        order = new Order
        {
            DateOfOrdering = DateTime.UtcNow,
            DateOfDelivery = createModel.DateOfDelivery ?? DateTime.UtcNow.AddDays(7),
            TotalPrice = total,
            OrderSupplierProducts = order.OrderSupplierProducts
        };

        return _orderRepository.CreateOrder(order);
    }
}