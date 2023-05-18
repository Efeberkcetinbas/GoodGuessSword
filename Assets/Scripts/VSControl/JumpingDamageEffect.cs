using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class JumpingDamageEffect : MonoBehaviour
{
    [SerializeField] private GameObject damagePrefab;
    public void StartCoinMove(Transform pointPos,string opt,int damageAmount,Color color)
    {
        GameObject coin=Instantiate(damagePrefab,pointPos.transform.position,damagePrefab.transform.rotation);
        coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().color=color;
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=opt + damageAmount.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    }
}
