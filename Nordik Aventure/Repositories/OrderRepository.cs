using GestBibli.Objects;
using Microsoft.EntityFrameworkCore;
using Nordik_Aventure.Objects.Models;

namespace Nordik_Aventure.Repositories;

public class OrderRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public OrderRepository()
    {
        
    }

    public GenericResponse<Order> GetOrderById(int id)
    {
        var order = _context.Orders.Where(order => order.OrderId == id)
            .Include(order => order.TotalPrice)
            .Include(order => order.DateOfOrdering)
            .Include(order => order.DateOfDelivery)
            .Include(order => order.OrderSupplierProducts).ToList()
            .SingleOrDefault();

        return new GenericResponse<Order>(order);
    }

    public GenericResponse<List<Order>> GetAllOrders()
    {
        var orders = _context.Orders
            .OrderBy(order => order.OrderId)
            .Include(order => order.TotalPrice)
            .Include(order => order.DateOfOrdering)
            .Include(order => order.DateOfDelivery)
            .Include(order => order.OrderSupplierProducts).ToList();
        return new GenericResponse<List<Order>>(orders);
    }
    
    public GenericResponse<Order> CreateOrder(Order order)
    {
        try
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return new GenericResponse<Order>(order);
        }
        catch (Exception e)
        {
            return new GenericResponse<Order>()
            {
                Code = 500,
                Data = null,
                Message = $"Erreur inattendue: {e}",
                Success = false
            };
        }
    }
}