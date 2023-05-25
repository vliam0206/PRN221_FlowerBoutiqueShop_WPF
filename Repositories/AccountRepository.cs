using DataAccess;
using Microsoft.Extensions.Configuration;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class AccountRepository : IAccountRepository
{
    public bool AdminLogin(string username, string password)
    {
        var adminAcc = GetAccountFromJson();
        if (adminAcc.Username == username && adminAcc.Password == password)
        {
            return true;
        }
        return false;
    }

    public bool CustomerLogin(string username, string password)
    {
        var customer = CustomerDAO.Instance.GetCustomerByEmail(username);
        if (customer !=null && customer.Password == password)
        {
            return true;
        }
        return false;
    }

    private Account GetAccountFromJson()
    {
        IConfiguration config = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        string username = config["AdminAccount:Username"];
        string password = config["AdminAccount:Password"];
        Account acc = new Account
        {
            Username = username,
            Password = password
        };
        
        return acc;
    }
}
public class Account
{
    public string Username { get; set; }
    public string Password { get; set; }
}
