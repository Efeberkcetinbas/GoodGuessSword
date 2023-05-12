using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalDamageControl : MonoBehaviour
{
    public RivalData rivalData;
    public PlayerData playerData;

    public List<GameObject> Rivals=new List<GameObject>();

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.AddHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
    }

    private void Start() {
        //EventManager.Broadcast(GameEvent.OnRivalUpdate);

        for (int i = 0; i < Rivals.Count; i++)
        {
            Rivals[i].SetActive(false);
        }

        Rivals[rivalData.index].SetActive(true);
    }

    private void OnTakeRivalDamage()
    {
        //Particles
        rivalData.RivalHealth-=playerData.MaxDamageAmount;

        //UI Managerda Rival icin
        EventManager.Broadcast(GameEvent.OnRivalUpdate);

        if(rivalData.RivalHealth<=0)
        {
            rivalData.index++;
            EventManager.Broadcast(GameEvent.OnRivalUpdate);
            EventManager.Broadcast(GameEvent.OnRivalDead);
        }

        for (int i = 0; i < Rivals.Count; i++)
        {
            Rivals[i].SetActive(false);
        }

        Rivals[rivalData.index].SetActive(true);
    }

    private void OnPreventRivalDamage()
    {
        //Particles
    }
}
