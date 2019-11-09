using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : IAbility
{
    public RewindTime()
    {
    }

    public void IDisable()
    {

    }

    public void IExecute()
    {
        RewindManager.Instance.rewind();
    }

    // Start is called before the first frame update
    public void IStart()
    {

    }

    // Update is called once per frame
    public void IUpdate()
    {
    }
}
