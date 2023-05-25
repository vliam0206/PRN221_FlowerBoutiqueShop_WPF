using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    public IEnumerable<OrderDetail> GetAll() 
        => OrderDetailDAO.Instance.GetOrderDetailList();

    public IEnumerable<OrderDetail> GetDetailsByFlower(int flowerId)
        => OrderDetailDAO.Instance.GetOrderDetailByFlower(flowerId);

    public IEnumerable<OrderDetail> GetDetailsByOrder(int ordId)
        =>OrderDetailDAO.Instance.GetOrderDetailByOrder(ordId);

    public void Insert(OrderDetail detail) 
        => OrderDetailDAO.Instance.AddOrderDetail(detail);

    public void Update(OrderDetail detail)
        => OrderDetailDAO.Instance.UpdateDetail(detail);
}
