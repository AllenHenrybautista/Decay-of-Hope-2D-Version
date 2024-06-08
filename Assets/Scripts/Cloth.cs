using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : MonoBehaviour
{
    [SerializeField] private string clothName;
    [SerializeField] private string quantity;
    [SerializeField] private Sprite sprite;

    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = GameObject.Find("UI").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _inventoryManager.AddItem(clothName, quantity, sprite);
            Destroy(gameObject);
        }
    }
}
