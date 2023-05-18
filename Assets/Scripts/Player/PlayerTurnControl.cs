using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnControl : MonoBehaviour
{
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayersTurn,OnPlayersTurn);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayersTurn,OnPlayersTurn);
    }

    private void OnPlayersTurn()
    {
        Debug.Log("PLAYER TURN BEGIN");
    }
}
