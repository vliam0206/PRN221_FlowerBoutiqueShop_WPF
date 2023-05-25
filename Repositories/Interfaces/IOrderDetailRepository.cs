using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IOrderDetailRepository
{
    IEnumerable<OrderDetail> GetAll();
    IEnumerable<OrderDetail> GetDetailsByFlower(int flowerId);
    IEnumerable<OrderDetail> GetDetailsByOrder(int ordId);
    void Insert(OrderDetail detail);
    void Update(OrderDetail detail);
}
