using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusControl : MonoBehaviour
{
    public PlayerData playerData;
    public RivalData rivalData;
    public GameData gameData;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnDoVersus,OnDoVersus);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnDoVersus,OnDoVersus);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnNextLevel()
    {
        ChangeTurn(true,false);
    }

    private void OnDoVersus()
    {
        if(gameData.isPlayersTurn)
        {
            if((playerData.up && rivalData.up) || (playerData.down && rivalData.down) || (playerData.left && rivalData.left) || (playerData.right && rivalData.right) || playerData.center && rivalData.center)
            {
                DoNotDamageToRival();
                return;
            }


            else 
            {
                DoDamageToRival();
                return;
            }
                
        }

        if(gameData.isRivalsTurn)
        {
            if((playerData.up && rivalData.up) || (playerData.down && rivalData.down) || (playerData.left && rivalData.left) || (playerData.right && rivalData.right) || playerData.center && rivalData.center)
            {
                DoNotDamageToPlayer();
                return;
            }
            

            else
            {
                DoDamageToPlayer();
                return;
            }
                
        }

    }

    private void DoDamageToRival()
    {
        EventManager.Broadcast(GameEvent.OnTakeRivalDamage);
        Debug.Log("DAMAGE RIVAL");
        ChangeTurn(false,true);
        EventManager.Broadcast(GameEvent.OnRivalsTurn);
    }

    private void DoNotDamageToRival()
    {
        EventManager.Broadcast(GameEvent.OnPreventRivalDamage);
        Debug.Log("NOT DAMAGE RIVAL");
        ChangeTurn(false,true);
        EventManager.Broadcast(GameEvent.OnRivalsTurn);
    }

    private void DoDamageToPlayer()
    {
        EventManager.Broadcast(GameEvent.OnTakePlayerDamage);
        Debug.Log("DAMAGE PLAYER");
        ChangeTurn(true,false);
        EventManager.Broadcast(GameEvent.OnPlayersTurn);
    }

    private void DoNotDamageToPlayer()
    {
        EventManager.Broadcast(GameEvent.OnPreventPlayerDamage);
        Debug.Log("NOT DAMAGE PLAYER");
        ChangeTurn(true,false);
        EventManager.Broadcast(GameEvent.OnPlayersTurn);
    }


    private void ChangeTurn(bool playerTurn,bool rivalTurn)
    {
        gameData.isPlayersTurn=playerTurn;
        gameData.isRivalsTurn=rivalTurn;
    }
    
}
