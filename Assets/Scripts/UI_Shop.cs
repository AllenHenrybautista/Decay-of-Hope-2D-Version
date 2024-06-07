using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;


    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("Item");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void CreateItemButton(Sprite itemsprite, string itemName, int itemCost)
    {

    }

}
