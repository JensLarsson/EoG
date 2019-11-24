using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Grabbable : MonoBehaviour, IGrabbable
{
    public enum GrabEffect { NoEffect = 0, SlowDown };


    public (GrabEffect, float) GetGrabEffect()
    {
        return (GrabEffect.NoEffect, 0.0f);
    }

    public void Grab()
    {
    }

    public void Release()
    {
    }
}
