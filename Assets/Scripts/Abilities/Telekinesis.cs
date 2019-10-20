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

    public void IExecute()
    {
        if (grabbedObject == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Grabbable")
            {
                grabbedObject = hit.collider.transform;
                grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
            grabbedObject = null;
        }
    }

    // Start is called before the first frame update
    public void IStart()
    {

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
