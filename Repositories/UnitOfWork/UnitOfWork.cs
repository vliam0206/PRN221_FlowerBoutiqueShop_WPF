using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IFlowerBouquetRepository _flowerBouquetRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISuplierRepository _suplierRepository;

    public UnitOfWork(IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IFlowerBouquetRepository flowerBouquetRepository,
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        ICategoryRepository categoryRepository,
        ISuplierRepository suplierRepository)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
        _flowerBouquetRepository = flowerBouquetRepository;
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _categoryRepository = categoryRepository;
        _suplierRepository = suplierRepository;
    }

    public IAccountRepository AccountRepository => _accountRepository;

    public ICustomerRepository CustomerRepository => _customerRepository;

    public IFlowerBouquetRepository FlowerBouquetRepository => _flowerBouquetRepository;

    public IOrderRepository OrderRepository => _orderRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;

    public ISuplierRepository SupplierRepository => _suplierRepository;

    public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;
}
