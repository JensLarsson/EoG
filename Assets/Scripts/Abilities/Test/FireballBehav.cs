using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehav : DestructibleObject
{
    [SerializeField] float pushForce;
    [SerializeField] public int bounces;
    [SerializeField] public int dmg;
    [SerializeField] AttackType attackType;

    int currentBounces = 0;

    public override void onDestroy()
    {
        base.onDestroy();
    }
    public override void takeDamage(AttackType attackType, int damage)
    {
        base.takeDamage(attackType, damage);
    }
    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);
    }
    public void push(Transform tran, bool goRight)
    {
        if (goRight)
        {
            GetComponent<Rigidbody2D>().AddForce(tran.right * pushForce);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(-tran.right * pushForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<DestructibleObject>() != null)
        {
            DestructibleObject DO = collision.gameObject.GetComponent<DestructibleObject>();
            if (DO.faction != faction)
            {
                DO.takeDamage(attackType, dmg);
                onDestroy();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        currentBounces++;
        if(bounces <= currentBounces)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        //if(GetComponent<Rigidbody2D>().velocity.y == 0)
        //{
        //    test++;
        //}
        Debug.Log(currentBounces);
    }
}
