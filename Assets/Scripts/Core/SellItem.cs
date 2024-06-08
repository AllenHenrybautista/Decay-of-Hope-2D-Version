using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem : MonoBehaviour
{
    public Wallet wallet;
    public ItemManager itemManager;
    public PlayerInventory inventoryManager;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Sell()
    {
        if (inventoryManager.inventory.Contains(itemManager.displayedCloth) && itemManager.displayedCloth.isPurchased)
        {
            wallet.money += itemManager.displayedCloth.sellvalue;
            inventoryManager.inventory.Remove(itemManager.displayedCloth);
            itemManager.displayedCloth.isPurchased = false;
            Debug.Log("Item Sold: " + itemManager.displayedCloth.name + " for " + itemManager.displayedCloth.sellvalue + " dollars");
            audioManager.PlaySFX(audioManager.sell);
        }
    }

    public void addMoney()
    {
        wallet.money += 10;
        audioManager.PlaySFX(audioManager.addMoney);
    }
}


