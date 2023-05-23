using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs;

public class OrderDetailDAO
{
    private readonly FUFlowerBouquetManagementContext _dbcontext;

    public OrderDetailDAO(FUFlowerBouquetManagementContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public IEnumerable<OrderDetail> GetOrderDetailList()
    {
        return _dbcontext.OrderDetails.ToList();
    }
    public OrderDetail GetOrderDetailByOrderId(int orderId, int bouquetId)
    {
        return _dbcontext.OrderDetails.SingleOrDefault(d =>d.OrderId == orderId && d.FlowerBouquetId == bouquetId);
    }
    //public IEnumerable<OrderDetail> GeOrderDetailByName(DateTime orderDate)
    //{
    //    return _dbcontext.OrderDetails.Where(o => o.OrderDate == orderDate).ToList();
    //}
    public void AddOrderDetail(OrderDetail detail)
    {
        var eDetail = GetOrderDetailByOrderId(detail.OrderId, detail.FlowerBouquetId);
        if (eDetail == null)
        {
            _dbcontext.OrderDetails.Add(detail);
        }
        else
        {
            throw new Exception("The order detail is already existed. (ID duplicated)");
        }
    }
    public void UpdateDetail(OrderDetail detail)
    {
        var eDetail = GetOrderDetailByOrderId(detail.OrderId, detail.FlowerBouquetId);
        if (eDetail != null)
        {
            _dbcontext.Entry<OrderDetail>(detail).State = EntityState.Modified;
        }
        else
        {
            throw new Exception("The order detail is not exist.");
        }
    }
    public void DeleteDetail(OrderDetail detail)
    {
        var eDetail = GetOrderDetailByOrderId(detail.OrderId, detail.FlowerBouquetId);
        if (eDetail != null)
        {
            _dbcontext.OrderDetails.Remove(detail);
        }
        else
        {
            throw new Exception("The order detail is not exist.");
        }
    }
}
