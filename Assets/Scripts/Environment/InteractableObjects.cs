using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField] UnityEvent _onInteractionTriggered;
    [SerializeField] GameObject messageBox;
    [SerializeField] TMPro.TextMeshProUGUI messageText;
    public string[] messages;
    public int currentMessage = 0;
    public bool isMessageActive = false;

    public bool inRange;

    public bool IsActive() => true;

    private void Start()
    {
        HideMessage();
    }

    public void OnInteraction()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && !isMessageActive)
        {
            if (inRange)
            {
                _onInteractionTriggered?.Invoke();
                if (messages.Length > 0)
                    ShowMessage(messages);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    public void ShowMessage(string[] newMessages)
    {
        messages = newMessages;
        currentMessage = 0;
        messageText.text = messages[currentMessage];
        messageBox.SetActive(true);
        isMessageActive = true;
    }

    public void HideMessage()
    {
        messageBox.SetActive(false);
        isMessageActive = false;
    }

    public void ShowNextMessage()
    {
        currentMessage++;
        if (currentMessage >= messages.Length)
        {
            HideMessage();
        }
        else
        {
            messageText.text = messages[currentMessage];
        }
    }

    public void ShowMessage(string newMessage)
    {
        string[] message = new string[1];
        message[0] = newMessage;
        ShowMessage(message);
    }

    public void ShowMessage(string newMessage, float duration)
    {
        ShowMessage(newMessage);
        StartCoroutine(MessageDuration(duration));
    }

    public IEnumerator MessageDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideMessage();
    }
}
