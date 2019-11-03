using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructibleObject : MonoBehaviour
{
    public int health;
    public enum AttackType 
    {
    Physical,
    Fire
    };

    public virtual void takeDamage(int damage)
    {
        health -= damage;
        healthCheck();
    }
    public virtual void takeDamage(AttackType attackType, int damage)
    {

        health -= damage;
        healthCheck();
    }
    public void healthCheck()
    {
        if(health <= 0)
        {
            onDestroy(); 
        }
    }
    public virtual void onDestroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
