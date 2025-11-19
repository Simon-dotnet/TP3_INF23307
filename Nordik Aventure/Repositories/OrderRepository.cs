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
        var order = _context.Orders
            .Where(o => o.OrderId == id)
            .Include(o => o.OrderSupplierProducts)
            .ThenInclude(osp => osp.Product)
            .Include(o => o.OrderSupplierProducts)
            .ThenInclude(osp => osp.Supplier)
            .SingleOrDefault();

        return new GenericResponse<Order>(order);
    }


    public GenericResponse<List<Order>> GetAllOrders()
    {
        var orders = _context.Orders
            .Include(o => o.OrderSupplierProducts)
            .ThenInclude(osp => osp.Product)
            .Include(o => o.OrderSupplierProducts)
            .ThenInclude(osp => osp.Supplier)
            .OrderBy(o => o.OrderId)
            .ToList();

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