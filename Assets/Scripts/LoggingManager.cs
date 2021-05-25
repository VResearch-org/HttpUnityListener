using System;
using UnityEngine;
using UnityEngine.UI;

public class LoggingManager : MonoBehaviour {
    public enum Type { Undefined, Animals, Contamination, Control, Hoarding, Relaxation, Symmetry, Thoughts};
    public static EventContainer eventContainer;

    internal static void LogObjectData(string callerName, string action, Type type = Type.Undefined, string handString = "N/A")
    {
        var eventTime = DateTime.Now;
        var line = eventTime.ToString("HH-mm-ss-ff") + "; " + callerName + "; " + action + "; " + handString + "; " + type + "; ";
        Debug.Log(line);
        eventContainer.AddEvent(eventTime, callerName, action, handString, type);
    }
    void Awake()
    {
        eventContainer = new EventContainer();
    }
    internal static string GetDisplayText()
    {
        return eventContainer.GetDisplayText();
    }

    internal static string GetContainer()
    {
        return eventContainer.GetContainer();
    }
}
