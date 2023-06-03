using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalDamageControl : MonoBehaviour
{
    public RivalData rivalData;
    public PlayerData playerData;
    public GameData gameData;

    public List<GameObject> RivalsCharacters=new List<GameObject>();
    public List<GameObject> RivalsModels=new List<GameObject>();

    [Header("Damage Effect")]
    [SerializeField] private JumpingDamageEffect jumpingDamage;
    [SerializeField] private Transform pointPos;
    [SerializeField] private ParticleSystem deadEffect;


    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.AddHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);        
        EventManager.AddHandler(GameEvent.OnRivalDeadEffect,OnRivalDeadEffect);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnRivalDeadEffect,OnRivalDeadEffect);
    }

    private void Start() {
        //EventManager.Broadcast(GameEvent.OnRivalUpdate);

        for (int i = 0; i < RivalsCharacters.Count; i++)
        {
            RivalsCharacters[i].SetActive(false);
            RivalsModels[i].SetActive(false);
        }

        RivalsCharacters[rivalData.index].SetActive(true);
        RivalsModels[rivalData.index].SetActive(true);
    }

    private void OnNextLevel()
    {
        rivalData.index++;
        for (int i = 0; i < RivalsCharacters.Count; i++)
        {
            RivalsCharacters[i].SetActive(false);
            RivalsModels[i].SetActive(false);
        }

        RivalsCharacters[rivalData.index].SetActive(true);
        RivalsModels[rivalData.index].SetActive(true);
        EventManager.Broadcast(GameEvent.OnUpdateRivalArmy);
    }

    private void OnRivalDeadEffect()
    {
        deadEffect.Play();
        RivalsCharacters[rivalData.index].SetActive(false);
        RivalsModels[rivalData.index].SetActive(false);
    }

    private void OnTakeRivalDamage()
    {
        //Particles
        rivalData.RivalHealth-=playerData.MaxDamageAmount;
        jumpingDamage.StartCoinMove(pointPos,"-",playerData.MaxDamageAmount,Color.red);

        //UI Managerda Rival icin
        EventManager.Broadcast(GameEvent.OnRivalUpdate);

        if(rivalData.RivalHealth<=0)
        {
            //rivalData.index++;
            gameData.isGameEnd=true;
            //EventManager.Broadcast(GameEvent.OnRivalUpdate);
            EventManager.Broadcast(GameEvent.OnRivalDead);
            //EventManager.Broadcast(GameEvent.OnNextLevel);
        }

        for (int i = 0; i < RivalsCharacters.Count; i++)
        {
            RivalsCharacters[i].SetActive(false);
        }

        RivalsCharacters[rivalData.index].SetActive(true);
    }

    private void OnPreventRivalDamage()
    {
        //Particles
    }
}
