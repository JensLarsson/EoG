using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : MonoBehaviour
{
    private int Health = 3; //Detta kan behöva ändras i fall andra beteenden läggs till
    protected VulnerabilityStateMachine vulMan;

    public Vulnerable(VulnerabilityStateMachine vulMan)
    {
        this.vulMan = vulMan;
    }
    public virtual void EnterState() //Detta kan vara överflödgt
    {

    }
    public virtual void LeaveState()
    {

    }
    public virtual void UpdateState()
    {

    }
    public virtual void Hit(int damage)
    {
        Health -= damage;
        vulMan.ChangeState(VulnerabilityStateMachine.Vulnerability.InVunerable);
    }
}

public class InVulnerable : Vulnerable
{
    float invulerableTime = 1.0f;
    float currentTime = 0.0f;
    public InVulnerable(VulnerabilityStateMachine vulMan, float invulerableTime) : base(vulMan)
    {
        this.invulerableTime = invulerableTime;
    }
    public override void EnterState()
    {
        currentTime = 0.0f;
        base.EnterState();
    }
    public override void Hit(int damage)
    {
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (
            invulerableTime > currentTime)
        {
            vulMan.ChangeState(VulnerabilityStateMachine.Vulnerability.Vulnerable);
        }
    }

}