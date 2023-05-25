using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
    Customer GetCustomer(int id);
    Customer GetCustomer(string username);
    IEnumerable<Customer> Search(string keyword);
    void Insert(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);    
}
