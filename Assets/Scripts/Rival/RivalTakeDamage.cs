using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RivalTakeDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private ParticleSystem damageParticle,shieldParticle;
    [SerializeField] private float duration=0.2f;


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

    private void OnTakeRivalDamage()
    {
        //damageParticle.Play();
        skinnedMeshRenderer.material.color=Color.red;
        transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
        Invoke("OnBackWhite",duration);
    }

    private void OnPreventRivalDamage()
    {
        //shieldParticle.Play();
        transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
    }

    private void OnBackWhite()
    {
        skinnedMeshRenderer.material.color=Color.white;
    }
}
