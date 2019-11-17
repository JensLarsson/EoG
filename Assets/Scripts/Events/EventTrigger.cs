using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [Header("Event Data")]
    public string[] eventCalls = new string[1];
    public EventParameter eventParameter;
    public UnityEvent action;

    [Header("Trigger Options")]
    [SerializeField] Actions postTriggerAction = Actions.Nothing;
    enum Actions { Nothing = 0, DeleteObject, DeactivateTrigger };
    [SerializeField] Interactions interactionType = Interactions.onButtonPress;
    enum Interactions { onEnter = 0, onButtonPress, onLeave }


    void Engage()
    {
        foreach (string eventCall in eventCalls)
        {
            if (eventCall != "")
            {
                EventManager.TriggerEvent(eventCall, eventParameter);
                action.Invoke();
            }
            switch (postTriggerAction)
            {
                case Actions.DeleteObject:
                    Destroy(this.gameObject);
                    break;

                case Actions.DeactivateTrigger:
                    Destroy(this);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactionType == Interactions.onEnter && collision.tag == "Player")
        {
            Engage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (interactionType == Interactions.onEnter && collision.gameObject.tag == "Player")
        {
            Engage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (interactionType == Interactions.onButtonPress && collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Engage();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (interactionType == Interactions.onButtonPress && collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Engage();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactionType == Interactions.onLeave && collision.gameObject.tag == "Player")
        {
            Engage();
        }
    }

}
