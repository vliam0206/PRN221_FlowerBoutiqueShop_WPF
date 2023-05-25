using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class OrderDAO
{
    // Use singleton pattern
    private static OrderDAO instance = null;
    private static readonly object instanceLock = new object();
    private OrderDAO() { }
    public static OrderDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }
    }
    public IEnumerable<Order> GetOrderList()
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Orders.Where(o => !o.OrderStatus.Equals("Deleted")).ToList();
    }
    public IEnumerable<Order> GetOrderByCustomerId(int cusId)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Orders.Where(o => !o.OrderStatus.Equals("Deleted"))
                                .Include(o => o.Customer)
                                .Where(c => c.CustomerId == cusId).ToList();
    }
    public Order GetOrderById(int id)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Orders.SingleOrDefault(o => o.OrderId == id);
    }
    public IEnumerable<Order> GeOrdertByOrderDate(DateTime orderDate)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Orders.Where(o => o.OrderDate == orderDate
                            && !o.OrderStatus.Equals("Deleted")).ToList();
    }
    public IEnumerable<Order> GeOrdertByOrderDate(DateTime startDate, DateTime endDate)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate
             && !o.OrderStatus.Equals("Deleted"))
            .ToList().OrderByDescending(o => o.Total);
    }
    public IEnumerable<Order> GeOrdertByShippedDate(DateTime shippedDate)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Orders.Where(o => o.ShippedDate == shippedDate
        && !o.OrderStatus.Equals("Deleted")).ToList();
    }
    public void AddOrder(Order order)
    {
        var eOrder = GetOrderById(order.OrderId);
        if (eOrder == null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Orders.Add(order);
            dbcontext.SaveChanges();
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
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Entry<Order>(order).State = EntityState.Modified;
            dbcontext.SaveChanges();
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
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Orders.Remove(order);
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The order is not exist.");
        }
    }
}
