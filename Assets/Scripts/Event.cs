using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Event
{
    public string name;
    public long eventTime;

    public string callerObject;
    public string hand;

    public LoggingManager.Type type;

    public Event(long eventTime, string callerObject, string name, string hand, LoggingManager.Type type)
    {
        this.name = name;
        this.eventTime = eventTime;
        this.callerObject = callerObject;
        this.hand = hand;
        this.type = type;
    }
}

[Serializable]
public class EventContainer
{
    public long callTime;
    public List<Event> events;

    public EventContainer()
    {
        callTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        events = new List<Event>();
    }

    internal void AddEvent(DateTime eventTime, string callerName, string action, string handString, LoggingManager.Type type)
    {
        Event newEvent = new Event(new DateTimeOffset(eventTime).ToUnixTimeSeconds(), callerName, action, handString, type);
        events.Add(newEvent);
    }

    internal string GetContainer()
    {
        callTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        var eventsToReturn = JsonUtility.ToJson(this);
        events = new List<Event>();

        return eventsToReturn;
    }

    internal string GetDisplayText()
    {
        return JsonUtility.ToJson(this, true);
    }
}
