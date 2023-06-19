using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public PlayerData playerData;
    public RivalData rivalData;

    [Header("Game Ending")]
    public GameObject successMenu;
    public GameObject failMenu;




    private void Awake() 
    {
        ClearData();
    }

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }
    
    void OnGameOver()
    {
        gameData.isGameEnd=true;
    }
    
    private void OnRivalDead()
    {
        StartCoroutine(OpenSuccess());
    }

    private IEnumerator OpenSuccess()
    {
        yield return new WaitForSeconds(3);
        successMenu.SetActive(true);
        successMenu.transform.DOScale(Vector3.one,0.5f);
    }

    private void OnNextLevel()
    {
        successMenu.transform.localScale=Vector3.zero;
        successMenu.SetActive(false);
        ResetDirections();
        gameData.levelIndex++;
        EventManager.Broadcast(GameEvent.OnMapUIUpdate);
    }

    void OnIncreaseScore()
    {
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,0.3f).OnUpdate(UpdateUI);
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
        //EventManager.Broadcast(GameEvent.OnPlayersTurn);

        gameData.isGameEnd=true;
    }

    
}
