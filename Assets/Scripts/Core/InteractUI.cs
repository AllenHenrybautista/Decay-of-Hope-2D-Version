using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InteractUI : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] UnityEvent _onInteractionTriggered;

    public bool inRange;
    public bool UIActive;
    public GameObject ShopUI;
    public GameObject InteractButton;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        OnInteract();
    }

    private void Start()
    {
        setupUI();
    }

    private void OnInteract()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (inRange && !UIActive)
                Openshop();
            else if (UIActive)
                CloseShop();
        }

        _onInteractionTriggered?.Invoke();
    }

    private void setupUI()
    {
        ShopUI.SetActive(false);
        UIActive = false;

        if (ShopUI == null)
            return;
    }

    public void Openshop()
    {
        Debug.Log("Shop Opened");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ShopUI.SetActive(true);
        UIActive = true;
        Time.timeScale = 0;
        audioManager.PlaySFX(audioManager.shopOpen);

    }

    public void CloseShop()
    {
        Debug.Log("Shop Closed");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ShopUI.SetActive(false);
        UIActive = false;
        Time.timeScale = 1;
        audioManager.PlaySFX(audioManager.shopClose);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Player in range");
            InteractButton.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractButton.SetActive(false);
            inRange = false;
        }
    }
}
