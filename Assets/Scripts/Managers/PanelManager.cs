using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,CharacterPanel,MapPanel,SettingsPanel;
    [SerializeField] private RectTransform VsPanel;

    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,CharacterX,CharacterY,MapX,MapY,duration;

    [SerializeField] private List<GameObject> characters=new List<GameObject>();

    [SerializeField] private GameObject backButton,settingsButton;

    public GameData gameData;


    

    private bool oneTime=true;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }
    
    private void Update() 
    {
        if(oneTime)
        {
            if(Input.touchCount>=1 && Input.GetTouch(0).position.y>Screen.height/2.5f)
            {
                StartPanel.gameObject.SetActive(false);
                Invoke("GameStart",1);
                VsPanel.DOScale(Vector2.one,0.2f).OnComplete(()=>EventManager.Broadcast(GameEvent.OnGameStart));
                oneTime=false;
            }
        }
    }

    private void GameStart()
    {
        gameData.isGameEnd=false;
    }

    private void OnNextLevel()
    {
        StartPanel.gameObject.SetActive(true);
        StartPanel.DOAnchorPos(Vector2.zero,0.1f);
        StartCoroutine(Blink(Fade.gameObject,Fade));
    }


  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        oneTime=true;

    }

    public void OpenSettingsMenu()
    {
        SettingsPanel.gameObject.SetActive(true);
        settingsButton.SetActive(false);
        SettingsPanel.transform.DOScale(Vector3.one,0.3f).SetEase(Ease.OutBounce);
    }

    public void CloseSettingsMenu()
    {
        SettingsPanel.transform.DOScale(Vector3.zero,0.1f).OnComplete(()=>{
            SettingsPanel.gameObject.SetActive(false);
            settingsButton.SetActive(true);
        });
    }


    public void OpenCharacterPanel()
    {
        oneTime=false;
        backButton.SetActive(false);
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        CharacterPanel.gameObject.SetActive(true);
        CharacterPanel.DOAnchorPos(Vector2.zero,duration).OnComplete(()=>StartCoroutine(CharactersAnimations(true)));
        
    }

    public void OpenMapPanel()
    {
        oneTime=false;
        //OnOpenMap Eventi Firlat bu sayede kamera map'in oldugu yere handler ile gider
        //EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        MapPanel.gameObject.SetActive(true);
        MapPanel.DOAnchorPos(Vector2.zero,duration);
    }

    public void BackToStart(bool isOnCharacter)
    {

        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartCoroutine(CharactersAnimations(false));
            StartPanel.DOAnchorPos(Vector2.zero,duration).OnComplete(()=>oneTime=true);
            CharacterPanel.DOAnchorPos(new Vector2(CharacterX,CharacterY),duration).OnComplete(()=>{
                CharacterPanel.gameObject.SetActive(false);
            });
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration).OnComplete(()=>oneTime=true);
            MapPanel.DOAnchorPos(new Vector2(MapX,MapY),duration).OnComplete(()=>MapPanel.gameObject.SetActive(false));
        }

        //EventManager.Broadcast(GameEvent.OnButtonClicked);
    }

    private IEnumerator CharactersAnimations(bool openCharacter)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].transform.localScale=Vector3.zero;
        }

        if(openCharacter)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].transform.DOScale(1,0.1f).SetEase(Ease.OutBounce);
                yield return new WaitForSeconds(0.25f);
            }
            backButton.SetActive(true);
        }
    }
}
