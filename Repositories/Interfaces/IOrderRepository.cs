using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetOrders(int cusId);
    IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate);
    IEnumerable<Order> GetOrdersByOrderDate(IEnumerable<Order> orders, DateTime orderDate);
    IEnumerable<Order> GetOrdersByOrderDate(DateTime startDate, DateTime endDate);
    IEnumerable<Order> GetOrdersByOrderDate(IEnumerable<Order> orders, DateTime startDate, DateTime endDate);
    Order GetOrder(int id);
    void Insert(Order order);
    void Update(Order order);
}
