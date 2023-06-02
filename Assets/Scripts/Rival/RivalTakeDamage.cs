using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RivalTakeDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private ParticleSystem shieldParticle;
    [SerializeField] private float duration=0.2f;
    [SerializeField] private Animator animator;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.AddHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
        EventManager.AddHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
    }

    private void OnTakeRivalDamage()
    {
        //damageParticle.Play();
        animator.SetTrigger("GetDamage");
        EventManager.Broadcast(GameEvent.OnGeneralTakeDamage);
        skinnedMeshRenderer.material.color=Color.red;
        transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
        Invoke("OnBackWhite",duration);
    }

    private void OnPreventRivalDamage()
    {
        animator.SetTrigger("NotDamage");
        EventManager.Broadcast(GameEvent.OnGeneralPreventDamage);
        shieldParticle.Play();
        //transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
    }

    private void OnBackWhite()
    {
        skinnedMeshRenderer.material.color=Color.white;
    }

    private void OnTakePlayerDamage()
    {
        animator.SetTrigger("NotDamage");
    }
}
