using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRangeIndicator : MonoBehaviour
{
    private void Start()
    {
        EventManager.Subscribe("SetRangeIndicator", SetRange);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe("SetRangeIndicator", SetRange);
    }

    public void SetRange(float range)
    {
        transform.localScale = Vector3.one * range * 2;
    }
    void SetRange(EventParameter eParam)
    {
        transform.localScale = Vector3.one * eParam.floatParam * 2;
    }
}
