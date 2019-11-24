﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour, IGrabbable
{
    public LayerMask collisionMask;
    Collision collisions;
    MovementBase movement;
    Rigidbody2D rigidBod;
    int direction = 1;
    public float standSpeed = 2.0f;
    public bool jumps = true;
    public Grabbable.GrabEffect grabEffect = Grabbable.GrabEffect.NoEffect;
    public float grabVariable = 2.0f;
    public float localTime = 1; //This is dumb and didn't work, should be changed
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
    public (Grabbable.GrabEffect, float) GetGrabEffect()
    {
        return (grabEffect, grabVariable);
    }
    IEnumerator Stand()
    {
        while (Mathf.Abs(transform.rotation.z) > rotationSnapMargin)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * localTime * standSpeed * direction));
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

    public void SetLocalTime(float f)
    {
        localTime = f;
    }

    void Update()
    {
        CollisionInfo collInfo = collisions.getCollisions();
        direction = collInfo.right ? -1 : collInfo.left ? 1 : direction;
        movement.Move(direction * localTime);
        if (jumps) Jump();
    }
    private void LateUpdate()
    {
        rigidBod.velocity = rigidBod.velocity * localTime;
    }
    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 1, collisionMask);
        if (hit)
        {
            movement.Jump();
        }
    }
}
