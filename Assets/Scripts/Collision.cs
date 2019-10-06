using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollisionInfo
{
    public bool above, below, left, right;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Collision : MonoBehaviour
{
    public LayerMask collisionMask;

    const float skinWidth = 0.015f;
    [Range(0.01f, 0.5f)] public float collisionDistance = 0.1f;
    [Range(2, 10)] public int horizontalRayCount = 4;
    [Range(2, 10)] public int verticalRayCount = 4;

    float horizontalRaySpacing, verticalRaySpacing;

    BoxCollider2D collider;
    Rigidbody2D rigBod;
    RaycastOrigins raycastOrigins;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigBod = GetComponent<Rigidbody2D>();
        CalculateRaySpacing();
    }

    public CollisionInfo getCollisions()
    {
        UpdateRaycastOrigins();
        Vector2 velocity = rigBod.velocity;
        CollisionInfo collisions = new CollisionInfo();
        HorizontalCollisions(velocity, ref collisions);
        VerticalCollisions(velocity, ref collisions);
        return collisions;
    }

    void VerticalCollisions(Vector3 velocity, ref CollisionInfo collisions)
    {
        float directionY = Mathf.Sign(velocity.y);              //vertical direction (1 eller -1)
        float rayLength = collisionDistance + skinWidth;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.botLeft : raycastOrigins.topLeft;   //sätter raycasts Start till toppen eller botten av BoxColliderns
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY; //sätter velocity till avståndet till det object som kolliderades med
                rayLength = hit.distance;                             //Sätter rayLength till velocity, så att de inte letar efter object under kollisionen
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    void HorizontalCollisions(Vector3 velocity, ref CollisionInfo collisions)
    {
        float directionX = Mathf.Sign(velocity.x);              //vertical direction (1 eller -1)
        float rayLength = collisionDistance + skinWidth;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.botLeft : raycastOrigins.botRight;   //sätter raycasts Start till Vänster eller Höger av BoxCollidern
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX; //sätter velocity till avståndet till det object som kolliderades med
                rayLength = hit.distance;                             //Sätter rayLength till velocity, så att de inte letar efter object under kollisionen
                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, botLeft, botRight;
    }
    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2); //flyttar origin in skinwidth

        raycastOrigins.botLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.botRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }
    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2); //flyttar origin in skinwidth

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
