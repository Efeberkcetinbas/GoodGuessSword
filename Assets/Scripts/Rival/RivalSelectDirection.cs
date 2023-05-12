using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalSelectDirection : MonoBehaviour
{
    public RivalData rivalData;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnRivalUp,OnRivalUp);
        EventManager.AddHandler(GameEvent.OnRivalDown,OnRivalDown);
        EventManager.AddHandler(GameEvent.OnRivalLeft,OnRivalLeft);
        EventManager.AddHandler(GameEvent.OnRivalRight,OnRivalRight);
        EventManager.AddHandler(GameEvent.OnRivalCenter,OnRivalCenter);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnRivalUp,OnRivalUp);
        EventManager.RemoveHandler(GameEvent.OnRivalDown,OnRivalDown);
        EventManager.RemoveHandler(GameEvent.OnRivalLeft,OnRivalLeft);
        EventManager.RemoveHandler(GameEvent.OnRivalRight,OnRivalRight);
        EventManager.RemoveHandler(GameEvent.OnRivalCenter,OnRivalCenter);
    }

    private void OnRivalUp()
    {
        CheckDirection(true,false,false,false,false);
    }

    private void OnRivalDown()
    {
        CheckDirection(false,true,false,false,false);
    }

    private void OnRivalLeft()
    {
        CheckDirection(false,false,true,false,false);
    }

    private void OnRivalRight()
    {
        CheckDirection(false,false,false,true,false);
    }

    private void OnRivalCenter()
    {
        CheckDirection(false,false,false,false,true);
    }

    private void CheckDirection(bool up,bool down,bool left,bool right,bool center)
    {
        rivalData.up=up;
        rivalData.down=down;
        rivalData.left=left;
        rivalData.right=right;
        rivalData.center=center;
    }
}
