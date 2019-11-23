using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float crawlingSpeed = 0.5f;
    public float jumpForce = 5.0f;
    public float increasedGravityScale = 2.5f;

    Rigidbody2D rigBod;
    Collision collisions;
    // Start is called before the first frame update
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collision>();
    }
    public void Move(float x, float y)
    {
        Vector2 velocity = new Vector2(x, y);
        CollisionInfo collInfo = collisions.getCollisions();
        rigBod.velocity = velocity;
    }
    public void Move(float x)
    {
        Vector2 velocity = new Vector2(x, rigBod.velocity.y);
        CollisionInfo collInfo = collisions.getCollisions();
        rigBod.velocity = velocity;
    }
    public void Jump()
    {
        if (collisions.getCollisions().below && rigBod.velocity.y < 0.01f)
        {
            Vector2 velocity = rigBod.velocity;
            velocity.y += jumpForce;
            rigBod.velocity = velocity;
        }
    }
    IEnumerator Passthrough(float time)
    {
        this.gameObject.layer = 10; //MAGIC NUMBER FOR PlayerPassthrough layer
        yield return new WaitForSeconds(time);
        this.gameObject.layer = 8;  //MAGIC NUMBER FOR Player layer
    }

    void GravityManipulation()
    {
        if (rigBod.velocity.y < 0)
        {
            rigBod.gravityScale = increasedGravityScale;
        }
        else
        {
            rigBod.gravityScale = 1;
        }
    }
}