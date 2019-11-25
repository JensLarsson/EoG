﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : IAbility
{
    IGrabbable grabbed;
    Transform grabbedObject;
    Rigidbody2D grabbedBody;
    Transform player;
    float maxRange = 4.0f;
    int previousLayer;
    private float uneditedSpeed; //temp

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
        if (grabbedBody == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<IGrabbable>(out grabbed))
            {
                if (Vector2.Distance(player.position, hit.collider.transform.position) < maxRange)
                {
                    grabbedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    grabbedObject = hit.collider.transform;
                    previousLayer = grabbedObject.gameObject.layer;
                    grabbedObject.gameObject.layer = 11; //Magic number for Grabbed Object layer
                    grabbed.Grab();
                    var grabEffect = grabbed.GetGrabEffect();
                    if (grabEffect.Item1 == Grabbable.GrabEffect.SlowDown)
                    { //This is dumb and should be changed
                        Movement movement = player.gameObject.GetComponent<Movement>();
                        Debug.Log(grabEffect.Item2);
                        uneditedSpeed = movement.speed;
                        movement.speed = uneditedSpeed * (1 / grabEffect.Item2);
                    }
                }
            }
        }
        else
        {
            var grabEffect = grabbed.GetGrabEffect();
            if (grabEffect.Item1 == Grabbable.GrabEffect.SlowDown)
            {
                player.gameObject.GetComponent<Movement>().speed = uneditedSpeed;
            }
                grabbedObject.gameObject.layer = previousLayer;
            grabbedBody = null;
            grabbedObject = null;
            grabbed.Release();
        }
    }


    // Update is called once per frame
    public void IUpdate()
    {
        if (grabbedObject != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 clampPos = Vector3.ClampMagnitude(mousePos - player.position, maxRange);
            clampPos += player.transform.position;
            clampPos = clampPos - grabbedObject.transform.position;
            if (Vector3.Distance(Vector3.zero, clampPos) > 0.1f)
            {
                clampPos *= 10;
            }

            grabbedBody.velocity = clampPos;


        }
    }
}
