using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementBehaviour : MonoBehaviour, IGrabbable
{
    public LayerMask collisionMask;
    Collision collisions;
    MovementBase movement;
    Rigidbody2D rigidBod;
    int direction = 1;
    public float standSpeed = 2.0f;
    [Range(0.001f, 1.0f)] float rotationSnapMargin = 0.04f;
    public void Grab()
    {
        rigidBod.constraints = RigidbodyConstraints2D.None;
    }
    public void Release()
    {
        rigidBod.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(Stand());
    }

    IEnumerator Stand()
    {
        while (Mathf.Abs(transform.rotation.z) > rotationSnapMargin)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * standSpeed * direction));
            yield return null;
        }
        transform.rotation = new Quaternion();
    }
    void Start()
    {
        movement = GetComponent<MovementBase>();
        collisions = GetComponent<Collision>();
        rigidBod = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        CollisionInfo collInfo = collisions.getCollisions();
        direction = collInfo.right ? -1 : collInfo.left ? 1 : direction;
        movement.Move(direction);
    }
}
