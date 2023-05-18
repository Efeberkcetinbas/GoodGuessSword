using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalTurnControl : MonoBehaviour
{
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnRivalsTurn,OnRivalsTurn);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnRivalsTurn,OnRivalsTurn);
    }

    private void OnRivalsTurn()
    {
        Debug.Log("RIVAL TURN BEGIN");
    }
}
