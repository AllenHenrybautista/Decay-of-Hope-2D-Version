using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    public ItemManager itemManager;
    public ClothingBase cloth;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ChangeCloth()
    {
        itemManager.displayedCloth = cloth;
        itemManager.Display();
        audioManager.PlaySFX(audioManager.select);
    }
}
