using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scale : MonoBehaviour
{
    public float weightGoal = 1.0f;
    public UnityEvent action;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().mass >= weightGoal)
        {
            Debug.Log("Mass reached");
            action.Invoke();
        }
    }
}
