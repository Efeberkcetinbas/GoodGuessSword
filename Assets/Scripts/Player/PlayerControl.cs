using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    public GameData gameData;
    public PlayerData playerData;

    [SerializeField] private Animator animator;

    void Start()
    {
        OnNextLevel();
    }
    void Update()
    {
        CheckMove();
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
        playerData.Health=playerData.TempHealth;
        EventManager.Broadcast(GameEvent.OnPlayerUpdateHealth);
    }

    private void CheckMove()
    {

        if(Input.touchCount>0 && !gameData.isGameEnd)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                firstPosition=touch.position;
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Moved)
            {
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Ended)
            {
                lastPosition=touch.position;

                

                if(Mathf.Abs(lastPosition.x-firstPosition.x)>Mathf.Abs(lastPosition.y-firstPosition.y))
                {
                    if(lastPosition.x>firstPosition.x)
                    {
                        EventManager.Broadcast(GameEvent.OnPlayerRight);
                    }
                    else
                    {
                        EventManager.Broadcast(GameEvent.OnPlayerLeft);
                    }
                }

                else
                {
                    if(lastPosition.y>firstPosition.y)
                    {
                        EventManager.Broadcast(GameEvent.OnPlayerUp);
                    }
                    
                    else if(lastPosition.x==firstPosition.x && lastPosition.y==firstPosition.y)
                    {
                        EventManager.Broadcast(GameEvent.OnPlayerCenter);
                    }
                    else 
                    {
                        EventManager.Broadcast(GameEvent.OnPlayerDown);
                    }
                    
                }
                EventManager.Broadcast(GameEvent.OnPlayerTouchScreen);
                StartCoroutine(OnCallVersus());
            }

            
        }
    }

    private IEnumerator OnCallVersus()
    {
        yield return new WaitForSeconds(0.1f);
        EventManager.Broadcast(GameEvent.OnDoVersus);
    }

    
}

  