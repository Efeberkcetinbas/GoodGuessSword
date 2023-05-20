using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnControl : MonoBehaviour
{
    public GameData gameData;
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
        StartCoroutine(PlayersTurn());
    }

    private IEnumerator PlayersTurn()
    {
        ChangeTurn(false,false);
        yield return new WaitForSeconds(2);
        ChangeTurn(true,false);
    }

    private void ChangeTurn(bool playerTurn,bool rivalTurn)
    {
        gameData.isPlayersTurn=playerTurn;
        gameData.isRivalsTurn=rivalTurn;
    }
}
