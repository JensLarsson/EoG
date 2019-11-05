using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vase : MonoBehaviour
{
    enum Liquid { Empty = 0, Water, Sand, Pork };
    Liquid liquid = Liquid.Empty;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Water>())
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            liquid = Liquid.Water;
            GetComponent<Rigidbody2D>().mass = 2;
            Destroy(collision.gameObject);
        }
    }
}
