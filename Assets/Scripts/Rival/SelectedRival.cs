using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectedRival : MonoBehaviour
{
    [SerializeField] private string specialName;
    [SerializeField] private int specialHealth;
    [SerializeField] private int damageAmount;

    public RivalData rivalData;

    /*Hepsinde ortak ozellikli animasyon istiyorsan Genel Rivala Ekle.
    Ama buraya ekleyerek farklı animasyon tepkileri de eklemis oluyorsun */


    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.AddHandler(GameEvent.OnRivalUpdate,OnRivalUpdate);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.RemoveHandler(GameEvent.OnRivalUpdate,OnRivalUpdate);
    }

    private void Start() 
    {
        rivalData.RivalsName=specialName;
        rivalData.RivalHealth=specialHealth;
        rivalData.TempHealth=specialHealth;
        rivalData.MaxDamageAmount=damageAmount;
        //StartCoroutine(WaitForBoss());
        EventManager.Broadcast(GameEvent.OnRivalUpdate);
    }

    private void OnRivalDead()
    {
        //Die Effect
        StartCoroutine(WaitForBoss());
    }

    private void OnRivalUpdate()
    {
        StartCoroutine(WaitForBoss());
    }

    private IEnumerator WaitForBoss()
    {
        yield return new WaitForSeconds(0.1f);
        rivalData.RivalsName=specialName;
        rivalData.RivalHealth=specialHealth;
        rivalData.MaxDamageAmount=damageAmount;
    }
}
