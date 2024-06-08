using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    // handles the display and management of dialogue messages in a game. this script is responsible for managing the dialogue system in a Unity game. It handles showing messages, dialogues, and HUD elements, ensuring smooth communication between characters and the player.
    public static MessageManager instance;
    public GameObject messageBox;
    public TMPro.TextMeshProUGUI messageText;
    public string[] messages;
    public int currentMessage = 0;
    public bool isMessageActive = false;

    private void Awake()
    {
        instance = this;
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
