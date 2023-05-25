using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class SupplierRepository : ISuplierRepository
{
    public IEnumerable<Supplier> GetAll() => SupplierDAO.Instance.GetSupplierList();    

    public Supplier GetSupplierById(int id) => SupplierDAO.Instance.GetSupplierById(id);    
}
