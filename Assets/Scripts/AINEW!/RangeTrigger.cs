using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTrigger : MonoBehaviour
{
    [SerializeField] private Collider2D _trigger;

    // Events to notify when player enters or exits the trigger range
    public delegate void PlayerTriggerAction();
    public event PlayerTriggerAction OnPlayerEnterTrigger;
    public event PlayerTriggerAction OnPlayerExitTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterTrigger?.Invoke(); // Invoke the event
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExitTrigger?.Invoke(); // Invoke the event
        }
    }
}


