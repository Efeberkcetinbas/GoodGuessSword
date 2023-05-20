using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalTurnControl : MonoBehaviour
{
    public GameData gameData;
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
        StartCoroutine(RivalsTurn());
    }

    private IEnumerator RivalsTurn()
    {
        ChangeTurn(false,false);
        yield return new WaitForSeconds(2);
        ChangeTurn(false,true);
        EventManager.Broadcast(GameEvent.OnResetHoles);
    }
    private void ChangeTurn(bool playerTurn,bool rivalTurn)
    {
        gameData.isPlayersTurn=playerTurn;
        gameData.isRivalsTurn=rivalTurn;
    }
}
