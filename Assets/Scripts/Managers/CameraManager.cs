using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    public CinemachineVirtualCamera cm;
    CinemachineBasicMultiChannelPerlin noise;
    public Transform cmCamera;

    Vector3 cameraInitialPosition;

    [Header("Shake Control")]
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;
    public float amplitudeGain=1;
    public float frequencyGain=1;

    private void Start() 
    {
        noise=cm.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if(noise == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise}");
        
    }
    
 

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,GameOver);
        EventManager.AddHandler(GameEvent.OnHit,OnHit);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,GameOver);
        EventManager.RemoveHandler(GameEvent.OnHit,OnHit);
    }

    void OnHit()
    {
        Noise();
    }

    public void Noise() 
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        StartCoroutine(ResetNoise(shakeTime));    
    }

    private IEnumerator ResetNoise(float duration)
    {
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;    
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


    #region CameraShaker
 
    

    private void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);

    }

    private void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    #endregion    
}
