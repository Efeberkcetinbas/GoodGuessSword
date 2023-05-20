using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectDirection : MonoBehaviour
{
    public PlayerData playerData;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.AddHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.AddHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.AddHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.AddHandler(GameEvent.OnPlayerCenter,OnPlayerCenter);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.RemoveHandler(GameEvent.OnPlayerDown,OnPlayerDown);
        EventManager.RemoveHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.RemoveHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.RemoveHandler(GameEvent.OnPlayerCenter,OnPlayerCenter);
    }

    private void OnPlayerUp()
    {
        CheckDirection(true,false,false,false,false);
    }

    private void OnPlayerDown()
    {
        CheckDirection(false,true,false,false,false);
    }

    private void OnPlayerLeft()
    {
        CheckDirection(false,false,true,false,false);
    }

    private void OnPlayerRight()
    {
        CheckDirection(false,false,false,true,false);
    }

    private void OnPlayerCenter()
    {
        CheckDirection(false,false,false,false,true);
    }

    private void CheckDirection(bool up,bool down,bool left,bool right,bool center)
    {
        playerData.up=up;
        playerData.down=down;
        playerData.left=left;
        playerData.right=right;
        playerData.center=center;

    }
}
