using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class FlowerBouquetDAO
{
    // Use singleton pattern
    private static FlowerBouquetDAO instance = null;
    private static readonly object instanceLock = new object();
    private FlowerBouquetDAO() { }
    public static FlowerBouquetDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new FlowerBouquetDAO();
                }
                return instance;
            }
        }
    }
    public IEnumerable<FlowerBouquet> GetBouquetList()
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.FlowerBouquets.ToList();
    }
    public FlowerBouquet GetBouquetById(int id)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.FlowerBouquets.SingleOrDefault(b => b.FlowerBouquetId == id);
    }
    public IEnumerable<FlowerBouquet> GetBouquetByName(string name)
    {
        var dbcontext = new FUFlowerBouquetManagementContext();
        return dbcontext.FlowerBouquets.Where(b => b.FlowerBouquetName.Contains(name)).ToList();
    }
    public void AddBouquet(FlowerBouquet bouquet)
    {
        var eBouquet = GetBouquetById(bouquet.FlowerBouquetId);
        if (eBouquet == null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.FlowerBouquets.Add(bouquet);
            dbcontext.SaveChanges();
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
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.Entry<FlowerBouquet>(bouquet).State = EntityState.Modified;
            dbcontext.SaveChanges();
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
            var dbcontext = new FUFlowerBouquetManagementContext();
            dbcontext.FlowerBouquets.Remove(bouquet);
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The flower bouquet is not exist.");
        }
    }

    public void SoftDelete(FlowerBouquet bouquet)
    {
        var eBouquet = GetBouquetById(bouquet.FlowerBouquetId);
        if (eBouquet != null)
        {
            var dbcontext = new FUFlowerBouquetManagementContext();
            eBouquet.FlowerBouquetStatus = 0;
            dbcontext.Entry<FlowerBouquet>(eBouquet).State = EntityState.Modified;
            dbcontext.SaveChanges();
        }
        else
        {
            throw new Exception("The flower bouquet is not exist.");
        }
    }
}
