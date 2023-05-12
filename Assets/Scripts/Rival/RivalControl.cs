using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalControl : MonoBehaviour
{

    private int index;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerTouchScreen,ChooseDirection);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerTouchScreen,ChooseDirection);
    }

    private void ChooseDirection()
    {
        index=Random.Range(0,5);
        switch(index)
        {
            case 0:
                EventManager.Broadcast(GameEvent.OnRivalUp);
                break;
            case 1:
                EventManager.Broadcast(GameEvent.OnRivalDown);
                break;
            case 2:
                EventManager.Broadcast(GameEvent.OnRivalLeft);
                break;
            case 3:
                EventManager.Broadcast(GameEvent.OnRivalRight);
                break;
            case 4:
                EventManager.Broadcast(GameEvent.OnRivalCenter);
                break;
        }
    }

    
}
