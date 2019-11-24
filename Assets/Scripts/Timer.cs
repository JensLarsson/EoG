using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct timerPackage
{
    [HideInInspector] public string StartCall;
    public float timerTime;
    public string EndCall;
}

public class Timer : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
        EventManager.Subscribe("StartTimer", StartTimer);
    }
    private void OnDisable()
    {
        EventManager.UnSubscribe("StartTimer", StartTimer);
    }
    public void StartTimer(EventParameter eParam)
    {
        StartCoroutine(RunTimer(eParam));
    }
    public void StartTimer(float time, string eventCall)
    {
        StartCoroutine(RunTimer(time, eventCall));
    }

    IEnumerator RunTimer(EventParameter eParam)
    {
        if (eParam.timerParam.StartCall != "")
        {
            EventManager.TriggerEvent(eParam.timerParam.StartCall, eParam);
        }
        text.enabled = true;
        float clock = 0.0f;
        while (clock < eParam.timerParam.timerTime)
        {
            clock += Time.deltaTime;
            text.text = ((float)System.Math.Round(clock * 100f) / 100f).ToString();
            yield return null;
        }
        text.enabled = false;
        EventManager.TriggerEvent(eParam.timerParam.EndCall, eParam);
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
