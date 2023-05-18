using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class LevelManager : MonoBehaviour
{

    [Header("Fader")]
    public List<RectTransform> rectTransforms=new List<RectTransform>();
    public List<Vector3> movePoints=new List<Vector3>();
    public List<Vector3> centerPoints=new List<Vector3>();


    [SerializeField] private GameObject Fader;

    private bool order=false;

    private void Start() 
    {
        OnNextLevel();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnNextLevel()
    {
        OnDoFade();
    }

    private void OnDoFade()
    {
        Fader.SetActive(true);
        order=!order;
        
        for (int i = 0; i < rectTransforms.Count; i++)
        {
            if(order)
            {
                rectTransforms[i].DOAnchorPos(movePoints[i],2).OnComplete(()=>{
                Fader.SetActive(false);
                });
            }
            else
            {
                rectTransforms[i].DOAnchorPos(centerPoints[i],2).OnComplete(()=>{
                    Fader.SetActive(false);
                });
            }

            
        }
    }

    
    
}
