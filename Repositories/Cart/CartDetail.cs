using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Cart;
public class CartDetail
{
    public FlowerBouquet Flower { get; set; }
    public string FlowerName { get; set; }
    public int Quantity { get; set; }
    public double Discount { get; set; }
}
