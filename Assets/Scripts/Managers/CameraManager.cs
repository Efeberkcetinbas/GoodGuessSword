using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    public CinemachineVirtualCamera cm;
    public CinemachineVirtualCamera cmv2;
    CinemachineBasicMultiChannelPerlin noise;
    CinemachineBasicMultiChannelPerlin noise2;
    public Transform cmCamera;

    Vector3 cameraInitialPosition;

    [Header("Shake Control")]
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;

    private void Start() 
    {
        noise=cm.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        noise2=cmv2.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        if(noise == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise}");

        if(noise2 == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise2}");
        
    }
    
 

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,GameOver);
        EventManager.AddHandler(GameEvent.OnHit,OnHit);
        EventManager.AddHandler(GameEvent.OnBallSpawn,OnBallSpawn);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.AddHandler(GameEvent.OnRivalDeadEffect,OnRivalDeadEffect);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,GameOver);
        EventManager.RemoveHandler(GameEvent.OnHit,OnHit);
        EventManager.RemoveHandler(GameEvent.OnBallSpawn,OnBallSpawn);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnRivalDead,OnRivalDead);
        EventManager.RemoveHandler(GameEvent.OnRivalDeadEffect,OnRivalDeadEffect);
    }

    void OnHit()
    {
        Noise(noise,5,5,.4f);
    }

    void OnRivalDead()
    {
        ChangeCameras(9,10);
        StartCoroutine(CallDeadEffect());
    }

    void OnRivalDeadEffect()
    {
        Noise(noise2,10,10,0.4f);
    }

    private IEnumerator CallDeadEffect()
    {
        yield return new WaitForSeconds(2);
        EventManager.Broadcast(GameEvent.OnRivalDeadEffect);
    }

    void OnBallSpawn()
    {
        Noise(noise,4,4,shakeTime);
    }

    public void Noise(CinemachineBasicMultiChannelPerlin noise, float amplitudeGain, float frequencyGain,float duration) 
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        StartCoroutine(ResetNoise(noise,duration));    
    }

    private IEnumerator ResetNoise(CinemachineBasicMultiChannelPerlin noise, float duration)
    {
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;    
    }

    private void OnNextLevel()
    {
        ChangeCameras(10,9);
    }

    

    

    
    public void ChangeFieldOfView(float fieldOfView,float resetFieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration).OnComplete(()=>{
            ResetFieldOfView(resetFieldOfView,0.1f);
        });
    }

    private void ResetFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }
   
    public void ResetCamera()
    {
        cm.m_Priority = 1;
    }

    void GameOver()
    {
        DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, 60, 0.5f).OnComplete(()=>
        {
            EventManager.Broadcast(GameEvent.OnUIGameOver);
        });
        
    }

    private void ChangeCameras(int cm1, int cm2)
    {
        cm.m_Priority=cm1;
        cmv2.m_Priority=cm2;
    }


    
}
