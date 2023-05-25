using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class OrderRepository : IOrderRepository
{
    public IEnumerable<Order> GetAll() => OrderDAO.Instance.GetOrderList();

    public Order GetOrder(int id) => OrderDAO.Instance.GetOrderById(id);

    public IEnumerable<Order> GetOrders(int cusId) 
        => OrderDAO.Instance.GetOrderByCustomerId(cusId);

    public IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate) 
        => OrderDAO.Instance.GeOrdertByOrderDate(orderDate);

    public IEnumerable<Order> GetOrdersByOrderDate(IEnumerable<Order> orders, DateTime orderDate) 
        => orders.Where(o => o.OrderDate == orderDate).ToList();

    public IEnumerable<Order> GetOrdersByOrderDate(DateTime startDate, DateTime endDate)
        => OrderDAO.Instance.GeOrdertByOrderDate(startDate, endDate);

    public IEnumerable<Order> GetOrdersByOrderDate(IEnumerable<Order> orders, DateTime startDate, DateTime endDate)
        => orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList().OrderBy(o => o.OrderDate);

    public void Insert(Order order) => OrderDAO.Instance.AddOrder(order);

    public void Update(Order order) => OrderDAO.Instance.UpdateOrder(order);
}
