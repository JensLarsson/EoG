using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePlayer : DestructibleObject
{
    public override void takeDamage(AttackType attackType, int damage)
    {        
     //   base.takeDamage(attackType, damage);
    }
    public override void takeDamage(int damage)
    {
       // base.takeDamage(damage);
    }
    public override void onDestroy()
    {
        base.onDestroy();
    }
    
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
