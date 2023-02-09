using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public enum Events
    {
        None,
        OnBeatChange,
        OnNoteHit,
        OnWin,
        ItemSpawn,
        EnnemySpawn,
        BiggerEnnemy,
        EndSong,
        LaunchWave,
        Boss,
        OnGoldWin,
        OnCombo5,
        OnCombo10,
        OnResetCombo,
        OnPlayerDeath,
        OnStartSong,
        ExplodeAll
    }
    private Dictionary<Events, Action> eventDictionnary;

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
                _instance.Init();
            }
            return _instance;
        }
    }
    public void Init()
    {
        eventDictionnary = new Dictionary<Events, Action>();
    }
    public static void StartListening(Events eventName, Action action)
    {

        //Debug.Log("Start Listening event : " + eventName + ". Action : " + action.ToString());
        if (Instance.eventDictionnary.TryGetValue(eventName, out Action eventToListen))
        {
            eventToListen += action;
            Instance.eventDictionnary[eventName] = eventToListen;
        }
        else
        {
            eventToListen += action;
            Instance.eventDictionnary.Add(eventName, action);
        }
    }
    public static void StopListening(Events eventName, Action action)
    {

        //Debug.Log("Stop Listening event : " + eventName + ". Action : " + action.ToString());
        if (Instance.eventDictionnary.TryGetValue(eventName, out Action eventToStopListen))
        {
            eventToStopListen -= action;
            Instance.eventDictionnary[eventName] = eventToStopListen;
            if (Instance.eventDictionnary[eventName] == null)
            {
                Instance.eventDictionnary.Remove(eventName);
                
            }
        }
    }

    public static void TriggerEvent(Events eventName)
    {
        Action eventToTrigger;
        //Debug.Log("Try to Invoke : " + eventName);
        if (Instance.eventDictionnary.TryGetValue(eventName, out eventToTrigger))
        {
            //Debug.Log("Action : " + eventToTrigger.ToString());
            eventToTrigger?.Invoke();
        }
    }



}