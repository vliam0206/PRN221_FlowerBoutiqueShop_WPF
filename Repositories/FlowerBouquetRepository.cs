using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class FlowerBouquetRepository : IFlowerBouquetRepository
{
    public void Delete(FlowerBouquet flower) => FlowerBouquetDAO.Instance.DeleteBouquet(flower);

    public IEnumerable<FlowerBouquet> GetAll() => FlowerBouquetDAO.Instance.GetBouquetList();

    public FlowerBouquet GetFlowerBouquet(int id) => FlowerBouquetDAO.Instance.GetBouquetById(id);

    public void Insert(FlowerBouquet flower) => FlowerBouquetDAO.Instance.AddBouquet(flower);

    public IEnumerable<FlowerBouquet> Search(string keyword) => FlowerBouquetDAO.Instance.GetBouquetByName(keyword);

    public void SoftDelete(FlowerBouquet flower) => FlowerBouquetDAO.Instance.SoftDelete(flower);

    public void Update(FlowerBouquet flower) => FlowerBouquetDAO.Instance.UpdateBouquet(flower);
}
