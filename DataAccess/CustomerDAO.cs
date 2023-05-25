using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class CustomerDAO
{
    // Use singleton pattern
    private static CustomerDAO instance = null;
    private static readonly object instanceLock = new object();
    private CustomerDAO() { }
    public static CustomerDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }
    }
    public IEnumerable<Customer> GetCustomerList()
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Customers.ToList();
    }
    public Customer GetCustomerById(int id)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);
    }
    public IEnumerable<Customer> GetCustomerByName(string name)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Customers.Where(c => c.CustomerName.Contains(name)).ToList();
    }
    public void AddCustomer(Customer customer)
    {
        var eCustomer = GetCustomerById(customer.CustomerId);
        if (eCustomer == null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Customers.Add(customer);
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The customer is already existed. (ID duplicated)");
        }
    }
    public void UpdateCustomer(Customer customer)
    {
        var eCustomer = GetCustomerById(customer.CustomerId);
        if (eCustomer != null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Entry<Customer>(customer).State = EntityState.Modified;
            dbcontext.SaveChanges();
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
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Customers.Remove(customer);
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The customer is not exist.");
        }
    }

    public Customer GetCustomerByEmail(string email)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.Customers.SingleOrDefault(c => c.Email.Contains(email));
    }
}
