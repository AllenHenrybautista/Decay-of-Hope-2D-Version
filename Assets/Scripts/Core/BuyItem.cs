using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public Wallet wallet;
    public ItemManager itemManager;
    public PlayerInventory playerInventory;
    public List<ClothingBase> inventory = new List<ClothingBase>();


    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Buy()
    {
        if (wallet.money >= itemManager.Cost)
        {
            // Check if the item already exists in the inventory
            ClothingBase existingItem = playerInventory.inventory.Find(i => i.id == itemManager.displayedCloth.id);

            if (existingItem != null)
            {
                Debug.LogWarning($"Item with ID {itemManager.displayedCloth.id} is already in the inventory.");
            }
            else
            {
                wallet.money -= itemManager.Cost;
                playerInventory.inventory.Add(itemManager.displayedCloth); // Add the item to the inventory
                itemManager.displayedCloth.isPurchased = true; // Mark the item as purchased
                Debug.Log("Item Bought: " + itemManager.displayedCloth.name + " for " + itemManager.Cost + " dollars");
                audioManager.PlaySFX(audioManager.buy);
            }
        }
        else
        {
            Debug.Log("Not enough money");
            audioManager.PlaySFX(audioManager.notEnoughMoney);
        }
    }
}
