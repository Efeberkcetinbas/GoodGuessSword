using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score,highscore,endingScore;

    public GameData gameData;
    public PlayerData playerData;
    public RivalData rivalData;
    [Header("Player Control")]
    public TextMeshProUGUI playerHealthText;
    public Image PlayerProgressBar;


    [Header("Rival Control")]
    public TextMeshProUGUI rivalHealthText;
    public TextMeshProUGUI rivalNameText;
    public TextMeshProUGUI rivalLevelText;

    [SerializeField] private List<Sprite> specialsImage=new List<Sprite>();

    public Image RivalProgressBar;
    public Image RivalImage;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnRivalUpdate,OnRivalUpdate);
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.AddHandler(GameEvent.OnPlayerUpdateHealth,OnPlayerUIUpdate);
        EventManager.AddHandler(GameEvent.OnTakePlayerDamage,OnPlayerUIUpdate);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnRivalUpdate,OnRivalUpdate);
        EventManager.RemoveHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.RemoveHandler(GameEvent.OnPlayerUpdateHealth,OnPlayerUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnTakePlayerDamage,OnPlayerUIUpdate);
    }

    
    void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    void OnPlayerUIUpdate()
    {
        playerHealthText.SetText(playerData.Health.ToString()+ " HP");
        PlayerProgressBar.DOFillAmount((float) playerData.Health/playerData.TempHealth,0.1f);
    }

    void OnRivalUIUpdate()
    {
        rivalHealthText.SetText(rivalData.RivalHealth.ToString());
        rivalHealthText.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    void OnRivalUpdate()
    {
        rivalNameText.SetText(rivalData.RivalsName.ToString());
        rivalHealthText.SetText(rivalData.RivalHealth.ToString() + " HP");
        RivalImage.sprite=specialsImage[rivalData.index];
        RivalProgressBar.DOFillAmount((float)rivalData.RivalHealth/rivalData.TempHealth,0.1f);
    }

    void OnRivalDead()
    {
        rivalData.TempHealth=rivalData.RivalHealth;
        rivalLevelText.SetText("Level " + (rivalData.index+1).ToString());
    }
}
