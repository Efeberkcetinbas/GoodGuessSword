using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem boomParticle;
    [SerializeField] private ParticleSystem ouchParticle;
    [SerializeField] private ParticleSystem shieldParticle;
    [SerializeField] private ParticleSystem naniParticle;
    [SerializeField] private ParticleSystem playersTurnParticle;
    [SerializeField] private ParticleSystem rivalsTurnParticle;
    [SerializeField] private ParticleSystem successParticle;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.AddHandler(GameEvent.OnPreventPlayerDamage,OnPreventPlayerDamage);
        EventManager.AddHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
        EventManager.AddHandler(GameEvent.OnPlayersTurn,OnPlayersTurn);
        EventManager.AddHandler(GameEvent.OnRivalsTurn,OnRivalsTurn);
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakePlayerDamage,OnTakePlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventPlayerDamage,OnPreventPlayerDamage);
        EventManager.RemoveHandler(GameEvent.OnPreventRivalDamage,OnPreventRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnPlayersTurn,OnPlayersTurn);
        EventManager.RemoveHandler(GameEvent.OnRivalsTurn,OnRivalsTurn);
        EventManager.RemoveHandler(GameEvent.OnRivalDead,OnRivalDead);
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

    private void OnPlayersTurn()
    {
        playersTurnParticle.Play();
    }

    private void OnRivalsTurn()
    {
        rivalsTurnParticle.Play();
    }

    private void OnRivalDead()
    {
        successParticle.Play();
    }
}
