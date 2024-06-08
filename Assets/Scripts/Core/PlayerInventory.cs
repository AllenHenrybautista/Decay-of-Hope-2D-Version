using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ClothingBase> inventory = new List<ClothingBase>();
    public Wallet wallet;

    public void Buy(ClothingBase item)
    {
        if (wallet.money >= item.cost)
        {
            wallet.money -= item.cost;
            inventory.Add(item);
            Debug.Log("Item Bought: " + item.name + " for " + item.cost + " dollars");
        }
    }

    public void Sell(ClothingBase item)
    {
        if (inventory.Contains(item) && item.isPurchased)
        {
            wallet.money += item.sellvalue;
            inventory.Remove(item);
            Debug.Log("Item Sold: " + item.name + " for " + item.sellvalue + " dollars");
        }
    }
}
