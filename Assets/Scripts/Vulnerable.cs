using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : MonoBehaviour
{
    public virtual void EnterState()
    {

    }
    public virtual void LeaveState()
    {

    }
    public virtual void UpdateState()
    {

    }
    public virtual void Hit()
    {

    }
}

public class InVulnerable : Vulnerable
{
    public override void Hit()
    {

    }


}