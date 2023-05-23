using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public interface IUnitOfWork
{
    public CategoryDAO CategoryDAO { get; }
    public CustomerDAO CustomerDAO { get; }
    public FlowerBouquetDAO FlowerBouquetDAO { get; }
    public OrderDAO OrderDAO { get; }
    public OrderDetailDAO OrderDetailDAO { get; }
    public SupplierDAO SupplierDAO { get; }
    public int SaveChange();
}
