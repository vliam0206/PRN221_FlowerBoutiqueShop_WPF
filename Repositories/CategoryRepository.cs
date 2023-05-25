using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class CategoryRepository : ICategoryRepository
{
    public IEnumerable<Category> GetAll() => CategoryDAO.Instance.GetCategoryList();    

    public Category GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id);    
}
