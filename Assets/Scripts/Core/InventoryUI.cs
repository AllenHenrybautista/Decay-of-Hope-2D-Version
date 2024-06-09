using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;

    [Header("Items")]
    public Image[] clothSlots; // Array to hold cloth slots

    void Update()
    {
        UpdateInventoryDisplay();
    }
    
    void UpdateInventoryDisplay()
    {
        for (int i = 0; i < playerInventory.inventory.Count; i++)
        {
            ClothingBase item = playerInventory.inventory[i];
            if(i < clothSlots.Length)
            {
                clothSlots[i].sprite = item.clothIcon;
                clothSlots[i].enabled = true;
            }
            else
            {
                Debug.LogWarning("Inventory slot exceeds available display slots.");
                break;
            }
        }
    }

    // Function to remove item from inventory based on index
    public void RemoveItemFromInventory(int index)
    {
        if (index >= 0 && index < playerInventory.inventory.Count)
        {
            playerInventory.inventory.RemoveAt(index);
            UpdateInventoryDisplay(); // Update the inventory display after removing the item
        }
        else
        {
            Debug.LogWarning("Invalid index provided for removing item from inventory.");
        }
    }
}
