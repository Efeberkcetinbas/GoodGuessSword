using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class LevelManager : MonoBehaviour
{
    public GameData gameData;
    [Header("Fader")]
    public List<RectTransform> rectTransforms=new List<RectTransform>();
    public List<Vector3> movePoints=new List<Vector3>();
    public List<Vector3> centerPoints=new List<Vector3>();


    [SerializeField] private GameObject Fader;
    [SerializeField] private RectTransform VsPanel;
    [SerializeField] private GameObject CM1,CM2;

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
        VsPanel.DOScale(new Vector2(0,0),0.1f);
        ChangeCameras(false,true);
        order=!order;
        
        for (int i = 0; i < rectTransforms.Count; i++)
        {
            if(order)
            {
                rectTransforms[i].DOAnchorPos(movePoints[i],2).OnComplete(()=>{
                    Fader.SetActive(false);
                    gameData.isGameEnd=false;
                    VsPanel.DOScale(new Vector2(1,1),0.3f);
                    ChangeCameras(true,false);
                });
            }
            else
            {
                rectTransforms[i].DOAnchorPos(centerPoints[i],2).OnComplete(()=>{
                    Fader.SetActive(false);
                    gameData.isGameEnd=false;
                    VsPanel.DOScale(new Vector2(1,1),0.3f);
                    ChangeCameras(true,false);
                });
            }
        }
    }

    private void ChangeCameras(bool cm1,bool cm2)
    {
        CM1.SetActive(cm1);
        CM2.SetActive(cm2);
    }

    
    
}
