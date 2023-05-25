using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IAccountRepository
{
    bool AdminLogin(string username, string password);
    bool CustomerLogin(string username, string password);
}
