using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ClothingBase> inventory = new List<ClothingBase>();
    public event Action<ClothingBase> OnItemSoldOrRemoved;
    public int inventoryLimit = 4;
    public Wallet wallet;

    public void Buy(ClothingBase item)
    {
        ClothingBase existingItem = inventory.Find(i => i.id == item.id);

        if (existingItem != null)
        {
            Debug.LogWarning($"Item with ID {item.id} is already in the inventory.");
            return;
        }

        if (inventory.Count >= inventoryLimit)
        {
            wallet.money += (inventory[0].sellvalue - item.cost);
            inventory.RemoveAt(0);
            inventory.Add(item);
            Debug.Log("Item Bought: " + item.name + " for " + item.cost + " dollars");
        }
        else
        {
            inventory.Add(item);
            Debug.Log("Item Bought: " + item.name + " for " + item.cost + " dollars");
        }
    }

    public void Sell(ClothingBase item)
    {
        if (inventory.Contains(item) && item.isPurchased)
        {
            Debug.Log("Selling item: " + item.name);
            wallet.money += item.sellvalue;
            inventory.Remove(item);
            OnItemSoldOrRemoved?.Invoke(item);
            Debug.Log("Item Sold: " + item.name + " for " + item.sellvalue + " dollars");
        }
    }
}
