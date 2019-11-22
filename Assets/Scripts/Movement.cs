using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collision))]
public class Movement : MonoBehaviour
{
    public Sprite standingSprite, crawlingSprite; // This is likely to change when animations are added.


    public float speed = 1.0f;
    public float crawlingSpeed = 0.5f;
    public float jumpForce = 5.0f;
    public float increasedGravityScale = 2.5f;

    bool collidingWithLadder; //I hate this, I hate this a lot. It should change in the future when I've expanded the collisions class.
    bool crawling = false;
    SpriteRenderer spriteRend;
    Rigidbody2D rigBod;
    BoxCollider2D collider; //Currently not used, but needs to be manipulated if the player shall have the ability to crawl.
    Collision collisions;
    [HideInInspector] public bool faceingRight;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rigBod = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        collisions = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = rigBod.velocity.y;

        //sry for messing with your script, but i need this :P /Erik
        if (x > 0) { faceingRight = true; }
        else if (x < 0) { faceingRight = false; }
        ManageCrawling(KeyCode.S);
        if (Input.GetButtonDown("Jump") && collisions.getCollisions().below) //Jumping
        {
            if (!crawling)
            {
                y = jumpForce;
            }
            else //Falling through platforms
            {
                StopCoroutine(Passthrough(0.0f));
                StartCoroutine(Passthrough(0.4f));
            }
        }


        Move(x, y);
        GravityManipulation();
    }

    void Move(float x, float y)
    {
        if (crawling && collisions.getCollisions().below)
        {
            x = x * crawlingSpeed;
        }
        else
        {
            x = x * speed;
        }
        if (collidingWithLadder)
        {
            y = Input.GetAxis("Vertical") * speed;
        }
        Vector2 velocity = new Vector2(x, y);
        CollisionInfo collInfo = collisions.getCollisions();
        rigBod.velocity = velocity;
    }

    void ManageCrawling(KeyCode input)
    {
        if (crawling && !Input.GetKey(input) && !collisions.getCollisions().above)
        {
            crawling = false;
            spriteRend.sprite = standingSprite;
            collider.size = new Vector2(1, 2);
            collisions.CalculateRaySpacing();
            transform.position += new Vector3(0, 0.5f); //Don't really like this solution, but it works so I'll leave it for now
        }
        if (Input.GetKeyDown(input))
        {
            crawling = true;
            spriteRend.sprite = crawlingSprite;
            collider.size = new Vector2(1, 1);
            collisions.CalculateRaySpacing();
            transform.position += new Vector3(0, -0.5f); //Don't really like this solution, but it works so I'll leave it for now
        }
    }

    IEnumerator Passthrough(float time)
    {
        this.gameObject.layer = 10; //MAGIC NUMBER FOR PlayerPassthrough layer
        yield return new WaitForSeconds(time);
        this.gameObject.layer = 8;  //MAGIC NUMBER FOR Player layer
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float vertical = Input.GetAxis("Vertical");
        if (collision.gameObject.tag == "Climb" && vertical != 0)
        {
            collidingWithLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            collidingWithLadder = false;
        }
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
