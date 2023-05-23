using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs;

public class CategoryDAO
{
    private readonly FUFlowerBouquetManagementContext _dbcontext;

    public CategoryDAO(FUFlowerBouquetManagementContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public IEnumerable<Category> GetCategoryList()
    {
        return _dbcontext.Categories.ToList();
    }
    public Category GetCategoryById(int id)
    {
        return _dbcontext.Categories.SingleOrDefault(c => c.CategoryId == id);
    }
}
