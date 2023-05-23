using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs;

public class OrderDAO
{
    private readonly FUFlowerBouquetManagementContext _dbcontext;

    public OrderDAO(FUFlowerBouquetManagementContext dbconetext)
    {
        _dbcontext = dbconetext;
    }
    public IEnumerable<Order> GetOrderList()
    {
        return _dbcontext.Orders.ToList();
    }
    public Order GetOrderById(int id)
    {
        return _dbcontext.Orders.SingleOrDefault(o => o.OrderId == id);
    }
    public IEnumerable<Order> GeOrdertByName(DateTime orderDate)
    {
        return _dbcontext.Orders.Where(o => o.OrderDate == orderDate).ToList();
    }
    public void AddOrder(Order order)
    {
        var eOrder = GetOrderById(order.OrderId);
        if (eOrder == null)
        {
            _dbcontext.Orders.Add(order);
        }
        else
        {
            throw new Exception("The order is already existed. (ID duplicated)");
        }
    }
    public void UpdateOrder(Order order)
    {
        var eOrder = GetOrderById(order.OrderId);
        if (eOrder != null)
        {
            _dbcontext.Entry<Order>(order).State = EntityState.Modified;
        }
        else
        {
            throw new Exception("The order is not exist.");
        }
    }
    public void DeleteOrder(Order order)
    {
        var eOrder = GetOrderById(order.OrderId);
        if (eOrder != null)
        {
            _dbcontext.Orders.Remove(order);
        }
        else
        {
            throw new Exception("The order is not exist.");
        }
    }
}
