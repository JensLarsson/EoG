using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        EventManager.Subscribe("StartTimer", StartTimer);
    }
    private void OnDisable()
    {
        EventManager.UnSubscribe("StartTimer", StartTimer);
    }

    public void StartTimer(EventParameter eParam)
    {
        StartCoroutine(RunTimer(eParam.floatParam, eParam));
    }
    public void StartTimer(float time, string eventCall)
    {
        StartCoroutine(RunTimer(time, eventCall));
    }

    IEnumerator RunTimer(float time, EventParameter eParam)
    {
        text.enabled = true;
        float clock = 0.0f;
        while (clock < time)
        {
            clock += Time.deltaTime;
            text.text = ((float)System.Math.Round(clock * 100f) / 100f).ToString();
            yield return null;
        }
        text.enabled = false;
        EventManager.TriggerEvent(eParam.stringParam, eParam);
    }

    IEnumerator RunTimer(float time, string eventCall)
    {
        text.enabled = true;
        float clock = 0.0f;
        while (clock < time)
        {
            clock += Time.deltaTime;
            text.text = ((float)System.Math.Round(clock * 100f) / 100f).ToString();
            yield return null;
        }
        text.enabled = false;
        EventParameter eParam = new EventParameter();
        EventManager.TriggerEvent(eventCall, eParam);
    }
}
