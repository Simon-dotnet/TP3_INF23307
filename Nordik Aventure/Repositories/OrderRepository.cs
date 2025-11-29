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
        try
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
        catch (Exception ex)
        {
            return new GenericResponse<Order>($"Erreur dans le get de la commande: {ex}", 500);
        }
  
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
    
    public GenericResponse<Order> UpdateOrderStatus(int orderId, string newStatus)
    {
        try
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return new GenericResponse<Order>("Commande introuvable", 404);

            order.Status = newStatus;
            _context.SaveChanges();

            return new GenericResponse<Order>(order);
        }
        catch (Exception e)
        {
            return new GenericResponse<Order>($"Erreur inattendue: {e}", 500);
        }
    }
    
    public GenericResponse<bool> DeleteOrder(int id)
    {
        try
        {
            var order = _context.Orders
                .Include(o => o.OrderSupplierProducts)
                .SingleOrDefault(o => o.OrderId == id);

            if (order == null)
                return new GenericResponse<bool>("Commande introuvable", 404);
            
            _context.OrderSupplierProducts.RemoveRange(order.OrderSupplierProducts);
            
            _context.Orders.Remove(order);
            _context.SaveChanges();

            return new GenericResponse<bool>(true);
        }
        catch (Exception e)
        {
            return new GenericResponse<bool>($"Erreur lors de la suppression: {e}", 500);
        }
    }
}