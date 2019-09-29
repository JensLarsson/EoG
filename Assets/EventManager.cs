using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct EventParameter //Add more Event Parameters here if needed
{
    public string stringParam;
    public int intParam;
}
// Right now when creating and subscribing Events, the function delegatet needs to pass an EventParameter, 
// I will add a Function Overloads to remove this requirement in the future

public static class EventManager
{
    private static Dictionary<string, Action<EventParameter>> eventDicionary = new Dictionary<string, Action<EventParameter>>();

    //Events subscribed to needs to be unsubsribed from as well, do this by adding a call for UnSubscribe() on the object's OnDissable call
    public static void Subscribe(string eventName, Action<EventParameter> subscription)
    {
        Action<EventParameter> thisEvent;
        if (eventDicionary.TryGetValue(eventName, out thisEvent))
        {
            //Add Event to an existing Action
            thisEvent += subscription;
            //Update Dictionary
            eventDicionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += subscription;
            //Create new action for the Dictionary
            eventDicionary.Add(eventName, thisEvent);
        }
    }

    public static void UnSubscribe(string eventName, Action<EventParameter> subscription)
    {
        Action<EventParameter> thisEvent;
        if (eventDicionary.TryGetValue(eventName, out thisEvent))
        {
            //Removes Event from existing Action
            thisEvent -= subscription;
            //Updates Dictionary
            eventDicionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventParameter param)
    {
        Action<EventParameter> thisEvent;
        if (eventDicionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
        else
        {
            Debug.LogError($"event name: {eventName} not found in eventDictionary");
        }
    }
}
