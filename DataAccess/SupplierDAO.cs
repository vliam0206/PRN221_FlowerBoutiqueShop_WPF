using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class SupplierDAO
{
    // Use singleton pattern
    private static SupplierDAO instance = null;
    private static readonly object instanceLock = new object();
    private SupplierDAO() { }
    public static SupplierDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new SupplierDAO();
                }
                return instance;
            }
        }
    }
    public IEnumerable<Supplier> GetSupplierList()
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Suppliers.ToList();
    }
    public Supplier GetSupplierById(int id)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Suppliers.SingleOrDefault(c => c.SupplierId == id);
    }
}
