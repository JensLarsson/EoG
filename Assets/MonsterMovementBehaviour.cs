using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementBehaviour : MonoBehaviour
{
    public LayerMask collisionMask;
    Collision collisions;
    MovementBase movement;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementBase>();
        collisions = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionInfo collInfo = collisions.getCollisions();
        direction = collInfo.right ? -1 : collInfo.left ? 1 : direction;
        movement.Move(direction);
    }
}
