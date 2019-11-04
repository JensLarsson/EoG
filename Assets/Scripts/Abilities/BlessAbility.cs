using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessAbility : MonoBehaviour, IAbility
{
    ParticleSystem particles;

    public BlessAbility(ParticleSystem p)
    {
        particles = p;
    }

    public void IDisable()
    {

    }

    public void IExecute()
    {

    }

    public void IStart()
    {

    }

    public void IUpdate()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        particles.transform.parent.right = targetPos - particles.transform.position;
        if (Input.GetButtonDown("Fire1"))
        {
            particles.emissionRate = 50;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            particles.emissionRate = 0;
        }
    }
}
