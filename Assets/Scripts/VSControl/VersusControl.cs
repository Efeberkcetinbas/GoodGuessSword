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
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnDoVersus,OnDoVersus);
    }

    private void OnDoVersus()
    {
        if(gameData.isPlayersTurn)
        {
            if((playerData.up && rivalData.up) || (playerData.down && rivalData.down) || (playerData.left && rivalData.left) || (playerData.right && rivalData.right) || playerData.center && rivalData.center)
                DoNotDamageToRival();


            else 
                DoDamageToRival();
        }

        if(gameData.isRivalsTurn)
        {
            if((playerData.up && rivalData.up) || (playerData.down && rivalData.down) || (playerData.left && rivalData.left) || (playerData.right && rivalData.right) || playerData.center && rivalData.center)
                DoNotDamageToPlayer();
            

            else 
                DoDamageToPlayer();
        }

    }

    private void DoDamageToRival()
    {
        Debug.Log("RIVA SALDIRILDI");
        gameData.isPlayersTurn=false;
    }

    private void DoNotDamageToRival()
    {
        Debug.Log("RIVALA SALDIRILMADI");
        gameData.isPlayersTurn=false;
    }

    private void DoDamageToPlayer()
    {
        Debug.Log("PLAYERA SALDIRILDI");
        gameData.isRivalsTurn=false;
    }

    private void DoNotDamageToPlayer()
    {
        Debug.Log("PLAYERA SALDIRILMADI");
        gameData.isRivalsTurn=false;
    }

    
}
