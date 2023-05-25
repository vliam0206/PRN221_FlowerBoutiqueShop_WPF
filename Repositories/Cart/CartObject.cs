using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Cart;

public class CartObject
{
    public Dictionary<int, int> Cart { get; set; }
    public CartObject()
    {
        Cart = new Dictionary<int, int>();
    }
    public void AddToCart(int itemID, int quantity = 1)
    {
        if (Cart.ContainsKey(itemID))
        {
            Cart[itemID] += quantity;
        }
        else
        {
            Cart.Add(itemID, quantity);
        }
    }
    public void RemoveFromCart(int itemID, int quantity = 1)
    {
        if (Cart.ContainsKey(itemID))
        {
            Cart[itemID] -= quantity;
            if (Cart[itemID] == 0)
            {
                Cart.Remove(itemID);
            }
        }
    }
}

