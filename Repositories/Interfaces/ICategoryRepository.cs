using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category GetCategoryById(int id);
}
