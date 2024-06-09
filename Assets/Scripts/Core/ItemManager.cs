using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour
{
    public ClothingBase displayedCloth;

    [Header("Item Information")]
    public string clothName;
    public string clothDescription;
    public Sprite clothIcon;
    public int Cost;
    public string clothType;
    public int sellValue;

    //Refs
    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemDescriptionUI;
    public Image clothIconUI;
    public TextMeshProUGUI costUI;
    public TextMeshProUGUI sellValueUI;
    public TextMeshProUGUI clothTypeUI;

    private void Start()
    {
        SetupClothes();
    }

    public void Display()
    {
        SetupClothes();
    }

    public void SetupClothes()
    {
        clothName = displayedCloth.name;
        clothDescription = displayedCloth.description;
        clothIcon = displayedCloth.clothIcon;
        Cost = displayedCloth.cost;
        clothType = displayedCloth.selectedClothType.ToString();
        sellValue = displayedCloth.sellvalue;

        itemNameUI.text = clothName;
        itemDescriptionUI.text = clothDescription;
        clothIconUI.sprite = clothIcon;
        costUI.text = Cost.ToString();
        sellValueUI.text = sellValue.ToString();
        clothTypeUI.text = clothType;
    }
}
