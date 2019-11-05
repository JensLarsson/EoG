using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : DestructibleObject, IAbility
{
    Transform grabbedObject;
    Transform player;
    float maxRange = 4.0f;

     GameObject fireball;
  

    public override void takeDamage(AttackType attackType, int damage){}
    public override void takeDamage(int damage){}
    public override void onDestroy(){}
    public FireBall(Transform playerTransform, GameObject fb)
    {
        player = playerTransform;
        fireball = fb;
    }

    public void IExecute()
    {
        GameObject newFireball = GameObject.Instantiate(fireball, player.transform.position, player.transform.rotation);
        FireballBehav fireBehav = newFireball.GetComponent<FireballBehav>();
        fireBehav.faction = faction;
        fireBehav.faction = Faction.Player;
        fireBehav.push(player.transform, player.GetComponent<Movement>().faceingRight);
    }

    // Start is called before the first frame update
    public void IStart()
    {

    }

    // Update is called once per frame
    public void IUpdate()
    {
        if (grabbedObject != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            grabbedObject.transform.position = player.position + Vector3.ClampMagnitude(mousePos - player.position, maxRange);
        }
    }

    public void IDisable()
    {

    }
}
