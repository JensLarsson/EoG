using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAbility : MonoBehaviour, IAbility
{
    public void IDisable()
    {

    }

    public void IExecute()
    {
        Debug.LogError("Hey, this ability does not have an implementation right now, sorry about that <3");
    }

    public void IStart()
    {
    }

    public void IUpdate()
    {
    }
}
