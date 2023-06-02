using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem boomParticle;
    [SerializeField] private ParticleSystem ouchParticle;
    [SerializeField] private ParticleSystem shieldParticle;
    [SerializeField] private ParticleSystem naniParticle;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.AddHandler(GameEvent.OnPreventPlayerDamage,OnPreventPlayerDamage);
        EventManager.AddHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventPlayerDamage,OnPreventPlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
    }

    private void OnTakePlayerDamage()
    {
        ouchParticle.Play();
    }

    private void OnTakeRivalDamage()
    {
        boomParticle.Play();
    }   

    private void OnPreventPlayerDamage()
    {
        shieldParticle.Play();
    }

    private void OnPreventRivalDamage()
    {
        naniParticle.Play();

    }
}
