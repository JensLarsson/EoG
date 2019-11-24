using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetInt : MonoBehaviour
{
    Animator animator;
    public string variableName;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetIntVarriable(int input)
    {
        animator.SetInteger(variableName, input);
        animator.SetTrigger("Start");
    }
}
