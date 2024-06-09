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
            ClothingBase existingItem = playerInventory.inventory.Find(i => i.id == itemManager.displayedCloth.id);
            if (existingItem != null)
            {
                Debug.LogWarning($"Item with ID {itemManager.displayedCloth.id} is already in the inventory.");
            }
            else if (playerInventory.HasClothType(itemManager.displayedCloth.selectedClothType))
            {
                Debug.LogWarning($"You already have a {itemManager.displayedCloth.selectedClothType} item equipped.");
            }
            else
            {
                wallet.money -= itemManager.Cost;
                playerInventory.inventory.Add(itemManager.displayedCloth);
                itemManager.displayedCloth.isPurchased = true;
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
