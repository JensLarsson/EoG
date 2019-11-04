using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleElement : DestructibleObject
{
    [SerializeField] AttackType[] weaknesses;
    public override void takeDamage(int damage)
    {
        Debug.LogWarning("woops seems like you did dmg to a object that can only be destroyed with a certain attack type");
    }
    public override void takeDamage(AttackType attackType, int damage)
    {
        for (int i = 0; i < weaknesses.Length; i++)
        {
            if (attackType == weaknesses[i])
            {
                base.takeDamage(damage);
                break;
            }
        }
    }
    public override void onDestroy()
    {
        base.onDestroy();
    }
}
