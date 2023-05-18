using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public PlayerData playerData;
    public RivalData rivalData;




    private void Awake() 
    {
        ClearData();
    }

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
    }
    
    void OnGameOver()
    {
        gameData.isGameEnd=true;
    }
    

    void OnIncreaseScore()
    {
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,1f).OnUpdate(UpdateUI);
    }

    

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUIUpdate);
    }

    

    

    
    void ClearData()
    {
        ResetDirections();
    }

    void OnNextLevel()
    {
        ResetDirections();
    }

    private void ResetDirections()
    {
        playerData.up=false;
        playerData.down=false;
        playerData.left=false;
        playerData.right=false;
        playerData.center=false;

        rivalData.up=false;
        rivalData.down=false;
        rivalData.left=false;
        rivalData.right=false;
        rivalData.center=false;
        
        gameData.isPlayersTurn=true;
        gameData.isRivalsTurn=false;

        gameData.isGameEnd=true;
    }

    
}
