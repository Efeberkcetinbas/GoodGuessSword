using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private ParticleSystem shieldParticle;
    [SerializeField] private float duration=0.2f;
    [SerializeField] private Animator animator;

    public PlayerData playerData;
    public RivalData rivalData;

    [Header("Damage Effect")]
    [SerializeField] private JumpingDamageEffect jumpingDamage;
    [SerializeField] private Transform pointPos;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.AddHandler(GameEvent.OnPreventPlayerDamage,OnPreventPlayerDamage);
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventPlayerDamage,OnPreventPlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
    }

    private void OnTakePlayerDamage()
    {
        //damageParticle.Play();
        
        playerData.Health-=rivalData.MaxDamageAmount;
        jumpingDamage.StartCoinMove(pointPos,"-",rivalData.MaxDamageAmount,Color.red);
        skinnedMeshRenderer.material.color=Color.red;
        animator.SetTrigger("GetDamage");
        //transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
        Invoke("OnBackWhite",duration);
    }

    private void OnPreventPlayerDamage()
    {
        shieldParticle.Play();
        animator.SetTrigger("NotDamage");
        //transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
    }

    private void OnBackWhite()
    {
        skinnedMeshRenderer.material.color=Color.white;
    }

    private void OnTakeRivalDamage()
    {
        animator.SetTrigger("NotDamage");
    }
}
