using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs;

public class CustomerDAO
{
    private readonly FUFlowerBouquetManagementContext _dbcontext;

    public CustomerDAO(FUFlowerBouquetManagementContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public IEnumerable<Customer> GetCustomerList()
    {
        return _dbcontext.Customers.ToList();
    }
    public Customer GetCustomerById(int id)
    {
        return _dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);
    }
    public IEnumerable<Customer> GetCustomerByName(string name)
    {
        return _dbcontext.Customers.Where(c => c.CustomerName.Contains(name)).ToList();
    }
    public void AddCustomer(Customer customer)
    {
        var eCustomer = GetCustomerById(customer.CustomerId);
        if (eCustomer == null)
        {
            _dbcontext.Customers.Add(customer);
        } else
        {
            throw new Exception("The customer is already existed. (ID duplicated)");
        }
    }
    public void UpdateCustomer(Customer customer)
    {
        var eCustomer = GetCustomerById(customer.CustomerId);
        if (eCustomer != null)
        {
            _dbcontext.Entry<Customer>(customer).State = EntityState.Modified;
        }
        else
        {
            throw new Exception("The customer is not exist.");
        }
    }
    public void DeleteCustomer(Customer customer)
    {
        var eCustomer = GetCustomerById(customer.CustomerId);
        if (eCustomer != null)
        {
            _dbcontext.Customers.Remove(customer);
        }
        else
        {
            throw new Exception("The customer is not exist.");
        }
    }
}
