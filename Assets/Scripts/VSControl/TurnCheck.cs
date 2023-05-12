using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCheck : MonoBehaviour
{   
    public GameData gameData;

    //Sword ve kalkan da aktif et.
    public void DoPlayersTurn()
    {
        gameData.isPlayersTurn=true;
    }

    public void DoRivalsTurn()
    {
        gameData.isRivalsTurn=true;
    }
}
