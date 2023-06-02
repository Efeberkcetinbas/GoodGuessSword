using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameEvent
{
    //Player
    OnPlayerTouchScreen,
    OnPlayerUp,
    OnPlayerDown,
    OnPlayerLeft,
    OnPlayerRight,
    OnPlayerCenter,
    OnTakePlayerDamage,
    OnPreventPlayerDamage,
    OnPlayerUpdateHealth,

    //Rival
    OnRivalUp,
    OnRivalDown,
    OnRivalLeft,
    OnRivalRight,
    OnRivalCenter,
    OnTakeRivalDamage,
    OnPreventRivalDamage,
    OnRivalUpdate,
    OnRivalDead,

    //Both
    OnGeneralTakeDamage,
    OnGeneralPreventDamage,

    //Platform
    OnResetHoles,


    //Game Management
    OnIncreaseScore,
    OnDoVersus,
    OnHit,
    OnBallSpawn,
    OnPlayersTurn,
    OnRivalsTurn,
    OnUIUpdate,
    OnUIGameOver,
    OnNextLevel,
    OnGameOver
}
public class EventManager
{
    private static Dictionary<GameEvent,Action> eventTable = new Dictionary<GameEvent, Action>();
    
    private static Dictionary<GameEvent,Action<int>> IdEventTable=new Dictionary<GameEvent, Action<int>>();
    //2 parametre baglayacagimiz ile bagladigimiz

    
    public static void AddHandler(GameEvent gameEvent,Action action)
    {
        if(!eventTable.ContainsKey(gameEvent))
            eventTable[gameEvent]=action;
        else eventTable[gameEvent]+=action;
    }

    public static void RemoveHandler(GameEvent gameEvent,Action action)
    {
        if(eventTable[gameEvent]!=null)
            eventTable[gameEvent]-=action;
        if(eventTable[gameEvent]==null)
            eventTable.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if(eventTable[gameEvent]!=null)
            eventTable[gameEvent]();
    }
    
}
