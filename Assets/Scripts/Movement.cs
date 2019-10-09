﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 5.0f;
    Rigidbody2D rigBod;
    Collision collisions;
    // Start is called before the first frame update
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = rigBod.velocity.y;

        if (Input.GetButtonDown("Jump"))
        {
            y = jumpForce;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            StopCoroutine(Passthrough(0.0f));
            StartCoroutine(Passthrough(0.3f));
        }
        Vector2 velocity = new Vector2(x * speed, y);
        CollisionInfo collInfo = collisions.getCollisions();
        rigBod.velocity = velocity; 
        Gravity();
    }

    IEnumerator Passthrough(float time)
    {
        this.gameObject.layer = 10; //MAGIC NUMBER FOR PlayerPassthrough layer
        yield return new WaitForSeconds(time);
        this.gameObject.layer = 8;  //MAGIC NUMBER FOR Player layer
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            Vector2 vec = new Vector2(rigBod.velocity.x, Input.GetAxis("Vertical") * speed);

            rigBod.velocity = vec;
        }
    }

    void Gravity()
    {
        if (rigBod.velocity.y < 0)
        {
            rigBod.gravityScale = 2;
        }
        else
        {
            rigBod.gravityScale = 1;
        }
    }

}
