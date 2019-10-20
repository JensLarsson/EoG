using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public float force = 1.0f;
    float timer = 0.0f;

    public GameObject prefab;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 / spawnRate)
        {
            timer = 0;
            GameObject gObject = Instantiate(prefab);
            gObject.transform.position = transform.position;
            gObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f) * force, Random.Range(0, force)));
        }
    }
}
