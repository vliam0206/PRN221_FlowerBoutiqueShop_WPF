using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs;

public class SupplierDAO
{
    private readonly FUFlowerBouquetManagementContext _dbcontext;

    public SupplierDAO(FUFlowerBouquetManagementContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public IEnumerable<Supplier> GetSupplierList()
    {
        return _dbcontext.Suppliers.ToList();
    }
    public Supplier GetSupplierById(int id)
    {
        return _dbcontext.Suppliers.SingleOrDefault(c => c.SupplierId == id);
    }

}
