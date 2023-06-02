using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,ImpactSound,CannonSound,ShieldSound;

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
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnGeneralTakeDamage,OnGeneralTakeDamage);
        EventManager.RemoveHandler(GameEvent.OnGeneralPreventDamage,OnGeneralPreventDamage);
        EventManager.RemoveHandler(GameEvent.OnBallSpawn,OnBallSpawn);
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
    

}
