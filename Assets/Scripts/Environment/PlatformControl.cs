using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformControl : MonoBehaviour
{

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material player,rival;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayersTurn,OnPlayersTurn);
        EventManager.AddHandler(GameEvent.OnRivalsTurn,OnRivalsTurn);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayersTurn,OnPlayersTurn);
        EventManager.RemoveHandler(GameEvent.OnRivalsTurn,OnRivalsTurn);
    }
    private void OnPlayersTurn()
    {
        meshRenderer.material.DOColor(player.color,2f);
    }

    private void OnRivalsTurn()
    {
        meshRenderer.material.DOColor(rival.color,2f);
    }
}
