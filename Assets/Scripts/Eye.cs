using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer sprRen;

    private void Start()
    {
        EventManager.Subscribe("EyeColour", ShiftColour);
    }

    private void OnDisable()
    {

        EventManager.UnSubscribe("EyeColour", ShiftColour);
    }

    void Update()
    {
        transform.right = target.position - transform.position;
    }

    void ShiftColour(EventParameter eventParam)
    {
        StartCoroutine(ColourShift(eventParam.colourParam, eventParam.floatParam));
    }

    IEnumerator ColourShift(Color colour, float time)
    {
        float ElapsedTime = 0.0f;
        float TotalTime = time;
        Color col = sprRen.color;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            sprRen.color = Color.Lerp(col, colour, (ElapsedTime / TotalTime));
            yield return null;
        }
    }
}
