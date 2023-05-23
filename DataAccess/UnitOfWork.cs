using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class UnitOfWork : IUnitOfWork
{    
    public CategoryDAO CategoryDAO => throw new NotImplementedException();

    public CustomerDAO CustomerDAO => throw new NotImplementedException();

    public FlowerBouquetDAO FlowerBouquetDAO => throw new NotImplementedException();

    public OrderDAO OrderDAO => throw new NotImplementedException();

    public OrderDetailDAO OrderDetailDAO => throw new NotImplementedException();

    public SupplierDAO SupplierDAO => throw new NotImplementedException();

    public int SaveChange()
    {
        throw new NotImplementedException();
    }
}
