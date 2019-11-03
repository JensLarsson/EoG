using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleColl : DestructibleObject
{
    public override void takeDamage(AttackType attackType, int damage)
    {
        base.takeDamage(attackType, damage);
    }
    public override void takeDamage(int damage)
    {
        Debug.Log("ouch!");
        base.takeDamage(damage);
    }
    public override void onDestroy()
    {
        base.onDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        takeDamage(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
