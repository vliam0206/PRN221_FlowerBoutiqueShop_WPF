using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class CustomerRepository : ICustomerRepository
{
    public void Delete(Customer customer) => CustomerDAO.Instance.DeleteCustomer(customer);

    public IEnumerable<Customer> GetAll() => CustomerDAO.Instance.GetCustomerList();            

    public Customer GetCustomer(int id) => CustomerDAO.Instance.GetCustomerById(id);

    public Customer GetCustomer(string username) => CustomerDAO.Instance.GetCustomerByEmail(username);

    public void Insert(Customer customer) => CustomerDAO.Instance.AddCustomer(customer);

    public IEnumerable<Customer> Search(string keyword) => CustomerDAO.Instance.GetCustomerByName(keyword);

    public void Update(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);
}
