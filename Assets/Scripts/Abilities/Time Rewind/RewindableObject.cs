using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            //send data to manager
            RewindManager.Instance.addObject(this.gameObject, GetComponent<Rigidbody2D>());
        }
        catch
        {
            Debug.LogError("rewined object was not correctly set up, does it have a rigidbody?");
        }

    }
}
