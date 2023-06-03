using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,ImpactSound,CannonSound,ShieldSound,ExplosionSound;

    AudioSource musicSource,effectSource;

    private bool hit;

    private void Start() 
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=0.4f;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.AddHandler(GameEvent.OnGeneralTakeDamage,OnGeneralTakeDamage);
        EventManager.AddHandler(GameEvent.OnGeneralPreventDamage,OnGeneralPreventDamage);
        EventManager.AddHandler(GameEvent.OnBallSpawn,OnBallSpawn);
        EventManager.AddHandler(GameEvent.OnRivalDeadEffect,OnRivalDeadEffect);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnGeneralTakeDamage,OnGeneralTakeDamage);
        EventManager.RemoveHandler(GameEvent.OnGeneralPreventDamage,OnGeneralPreventDamage);
        EventManager.RemoveHandler(GameEvent.OnBallSpawn,OnBallSpawn);
        EventManager.RemoveHandler(GameEvent.OnRivalDeadEffect,OnRivalDeadEffect);
    }

    
    void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }

    private void OnGeneralTakeDamage()
    {
        effectSource.PlayOneShot(ImpactSound);
    }

    private void OnGeneralPreventDamage()
    {
        effectSource.PlayOneShot(ShieldSound);
    }

    private void OnBallSpawn()
    {
        effectSource.PlayOneShot(CannonSound);
    }

    private void OnRivalDeadEffect()
    {
        effectSource.PlayOneShot(ExplosionSound);
    }
    

}
