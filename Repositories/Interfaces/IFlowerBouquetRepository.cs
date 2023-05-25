using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IFlowerBouquetRepository
{
    IEnumerable<FlowerBouquet> GetAll();
    FlowerBouquet GetFlowerBouquet(int id);
    IEnumerable<FlowerBouquet> Search(string keyword);
    void Insert(FlowerBouquet flower);
    void Update(FlowerBouquet flower);
    void Delete(FlowerBouquet flower);
    void SoftDelete(FlowerBouquet flower);
}
