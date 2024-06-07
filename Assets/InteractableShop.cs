using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



public class InteractableShop : MonoBehaviour
{
    public UnityEvent OnInteract;
    public GameObject ShopMenu;
    public bool inRange;


    private void Update()
    {
        if (inRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ShopMenu.SetActive(true);
            OnInteract.Invoke();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            ShopMenu.SetActive(false);
        }
            
    }
}
