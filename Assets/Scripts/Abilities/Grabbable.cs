using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Grabbable : MonoBehaviour, IGrabbable
{
    public int GrabEffect()
    {
        return 0;
    }

    public void Grab()
    {
    }

    public void Release()
    {
    }
}
