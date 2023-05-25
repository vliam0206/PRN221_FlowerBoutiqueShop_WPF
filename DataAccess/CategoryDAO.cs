using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class CategoryDAO
{
    // Use singleton pattern
    private static CategoryDAO instance = null;
    private static readonly object instanceLock = new object();
    private CategoryDAO() { }
    public static CategoryDAO Instance
    {
        get
        {
            lock(instanceLock)
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }
    }
    public IEnumerable<Category> GetCategoryList()
    {
        var dbContext = new FUFlowerBouquetManagementContext();
        return dbContext.Categories.ToList();
    }
    public Category GetCategoryById(int id)
    {
        var dbContext = new FUFlowerBouquetManagementContext();
        return dbContext.Categories.SingleOrDefault(c => c.CategoryId == id);
    }
}
