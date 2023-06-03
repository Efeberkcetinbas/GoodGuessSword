using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;


public class LevelManager : MonoBehaviour
{

    [SerializeField] private RectTransform VsPanel;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnRivalDead,OnRivalDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnRivalDead,OnRivalDead);
    }

    private void OnRivalDead()
    {
        VsPanel.localScale=Vector2.zero;
    }


    public void ButtonNextLevel()
    {
        EventManager.Broadcast(GameEvent.OnNextLevel);
    }

    

    
    
}
