using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class OrderDetailDAO
{
    // Use singleton pattern
    private static OrderDetailDAO instance = null;
    private static readonly object instanceLock = new object();
    private OrderDetailDAO() { }
    public static OrderDetailDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
        }
    }
    public IEnumerable<OrderDetail> GetOrderDetailList()
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.OrderDetails.ToList();
    }
    public OrderDetail GetOrderDetailById(int orderId, int bouquetId)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.OrderDetails.SingleOrDefault(d => d.OrderId == orderId && d.FlowerBouquetId == bouquetId);
    }
    public IEnumerable<OrderDetail> GetOrderDetailByFlower(int bouquetId)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.OrderDetails.Where(d => d.FlowerBouquetId == bouquetId).ToList();
    }
    public IEnumerable<OrderDetail> GetOrderDetailByOrder(int ordId)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.OrderDetails.Where(d => d.OrderId == ordId).ToList();
    }
    public void AddOrderDetail(OrderDetail detail)
    {
        var eDetail = GetOrderDetailById(detail.OrderId, detail.FlowerBouquetId);
        if (eDetail == null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.OrderDetails.Add(detail);
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The order detail is already existed. (ID duplicated)");
        }
    }
    public void UpdateDetail(OrderDetail detail)
    {
        var eDetail = GetOrderDetailById(detail.OrderId, detail.FlowerBouquetId);
        if (eDetail != null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Entry<OrderDetail>(detail).State = EntityState.Modified;
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The order detail is not exist.");
        }
    }
    public void DeleteDetail(OrderDetail detail)
    {
        var eDetail = GetOrderDetailById(detail.OrderId, detail.FlowerBouquetId);
        if (eDetail != null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.OrderDetails.Remove(detail);
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The order detail is not exist.");
        }
    }
}
