using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ATMControl : MonoBehaviour
{
    [SerializeField] private Transform playerAtm,rivalAtm,playerAtmSpawnPos,rivalAtmSpawnPos;

    [SerializeField] private Transform targetPlayerAtm,targetRivalAtm;

    [SerializeField] private GameObject money;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
    }

    private void OnTakePlayerDamage()
    {
        playerAtm.DOScale(new Vector3(70,70,70),0.3f).OnComplete(()=>playerAtm.DOScale(new Vector3(50,50,50),0.5f));
        GameObject atmMoney=Instantiate(money,playerAtmSpawnPos.position,transform.rotation);
        atmMoney.transform.DOLocalJump(targetPlayerAtm.position,1,1,1f).OnComplete(()=>Destroy(atmMoney));
    }

    private void OnTakeRivalDamage()
    {
        rivalAtm.DOScale(new Vector3(70,70,70),0.3f).OnComplete(()=>rivalAtm.DOScale(new Vector3(50,50,50),0.5f));
        GameObject atmMoney=Instantiate(money,rivalAtmSpawnPos.position,transform.rotation);
        atmMoney.transform.DOLocalJump(targetRivalAtm.position,1,1,1f).OnComplete(()=>{
            EventManager.Broadcast(GameEvent.OnIncreaseScore);
            Destroy(atmMoney);
        });
    }
}
