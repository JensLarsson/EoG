using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : IAbility
{
    Transform grabbedObject;
    Transform player;
    float maxRange = 4.0f;

    public Telekinesis(Transform playerTransform)
    {
        player = playerTransform;
    }

    // Start is called before the first frame update
    public void IStart()
    {
        EventParameter eParam = new EventParameter()
        {
            floatParam = maxRange
        };
        EventManager.TriggerEvent("SetRangeIndicator", eParam);
    }
    public void IDisable()
    {
        EventParameter eParam = new EventParameter()
        {
            floatParam = 0.0f
        };
        EventManager.TriggerEvent("SetRangeIndicator", eParam);
    }

    public void IExecute()
    {
        if (grabbedObject == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Grabbable")
            {
                if (Vector2.Distance(player.position, hit.collider.transform.position) < maxRange)
                {
                    grabbedObject = hit.collider.transform;
                    grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        else
        {
            grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
            grabbedObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            grabbedObject = null;
        }
    }


    // Update is called once per frame
    public void IUpdate()
    {
        if (grabbedObject != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            grabbedObject.transform.position = player.position + Vector3.ClampMagnitude(mousePos - player.position, maxRange);
        }
    }
}
