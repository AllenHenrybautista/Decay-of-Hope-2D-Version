using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloth", menuName = "Inventory/Cloth")]
public class ClothingBase : ScriptableObject
{
    public string id;
    public string name;
    public Sprite clothIcon;
    public int cost;
    public int sellvalue;
    public string description;

    public ClothType selectedClothType = new ClothType();

    public enum ClothType
    {
        Head,
        Body,
        Shoes
    }

    // Item usability status
    public bool isEquipped = false;
    public bool isPurchased = false;

    // New fields for equipment sprites and animator
    public Sprite[] sprites;
    public RuntimeAnimatorController animatorController;
}

