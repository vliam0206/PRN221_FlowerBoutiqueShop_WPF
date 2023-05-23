using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs;

public class FlowerBouquetDAO
{
    private readonly FUFlowerBouquetManagementContext _dbcontext;

    public FlowerBouquetDAO(FUFlowerBouquetManagementContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public IEnumerable<FlowerBouquet> GetBouquetList()
    {
        return _dbcontext.FlowerBouquets.ToList();
    }
    public FlowerBouquet GetBouquetById(int id)
    {
        return _dbcontext.FlowerBouquets.SingleOrDefault(b => b.FlowerBouquetId == id);
    }
    public IEnumerable<FlowerBouquet> GetBouquetByName(string name)
    {
        return _dbcontext.FlowerBouquets.Where(b => b.FlowerBouquetName.Contains(name)).ToList();
    }
    public void AddBouquet(FlowerBouquet bouquet)
    {
        var eBouquet = GetBouquetById(bouquet.FlowerBouquetId);
        if (eBouquet == null)
        {
            _dbcontext.FlowerBouquets.Add(bouquet);
        }
        else
        {
            throw new Exception("The flower bouquet is already existed. (ID duplicated)");
        }
    }
    public void UpdateBouquet(FlowerBouquet bouquet)
    {
        var eBouquet = GetBouquetById(bouquet.FlowerBouquetId);
        if (eBouquet != null)
        {
            _dbcontext.Entry<FlowerBouquet>(bouquet).State = EntityState.Modified;
        }
        else
        {
            throw new Exception("The flower bouquet is not exist.");
        }
    }
    public void DeleteBouquet(FlowerBouquet bouquet)
    {
        var eBouquet = GetBouquetById(bouquet.FlowerBouquetId);
        if (eBouquet != null)
        {
            _dbcontext.FlowerBouquets.Remove(bouquet);
        }
        else
        {
            throw new Exception("The flower bouquet is not exist.");
        }
    }
}
