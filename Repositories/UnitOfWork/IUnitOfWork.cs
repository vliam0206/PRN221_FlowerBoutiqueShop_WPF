using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWork;

public interface IUnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IFlowerBouquetRepository FlowerBouquetRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderDetailRepository OrderDetailRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ISuplierRepository SupplierRepository { get; }
}
