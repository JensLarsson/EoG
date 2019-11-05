using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 5.0f;
    Rigidbody2D rigBod;
    Collision collisions;
    [HideInInspector] public bool faceingRight;
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

        //sry for messing with your script, but i need this :P /Erik
        if (x > 0) { faceingRight = true; }
        else if(x < 0) { faceingRight = false;}

        if (Input.GetButtonDown("Jump"))
        {
            y = jumpForce;
        }
        if (Input.GetKey(KeyCode.S))
        {
            StopCoroutine(Passthrough(0.0f));
            StartCoroutine(Passthrough(0.4f));
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
            rigBod.gravityScale = 2.5f;
        }
        else
        {
            rigBod.gravityScale = 1;
        }
    }

}
