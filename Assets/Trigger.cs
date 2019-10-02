using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string eventCall;
    public EventParameter eventParameter;
    [SerializeField] actions action = actions.Nothing;
    enum actions { Nothing = 0, Delete, DeactivateTrigger };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.TriggerEvent(eventCall, eventParameter);
            switch (action)
            {
                case actions.Delete:
                    Destroy(this.gameObject);
                    break;

                case actions.DeactivateTrigger:
                    Destroy(this);
                    break;
            }


        }
    }
}
