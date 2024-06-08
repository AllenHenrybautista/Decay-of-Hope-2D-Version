using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Cloth", menuName = "Inventory/Cloth")]
public class ClothingBase : ScriptableObject
{
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
        shoes
    }

    //item usability status
    public bool isEquipped = false;
    public bool isPurchased = false;

}
