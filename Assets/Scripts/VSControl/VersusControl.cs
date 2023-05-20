using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class VersusControl : MonoBehaviour
{
    public PlayerData playerData;
    public RivalData rivalData;
    public GameData gameData;

    [Header("Animations")]
    //Interactions Eklenince Kaldir
    [SerializeField] private Animator playerAnima;
    [SerializeField] private Animator rivalAnim;

    [Header("Holes")]
    [SerializeField] private GameObject Ball;
    [SerializeField] private Transform[] SpawnPos;
    [SerializeField] private Transform PlayerTarget;
    [SerializeField] private Transform RivalTarget;
    [SerializeField] private List<GameObject> ClosedHoles=new List<GameObject>();


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnDoVersus,OnDoVersus);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnResetHoles,OnResetHoles);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnDoVersus,OnDoVersus);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnResetHoles,OnResetHoles);
    }

    private void OnNextLevel()
    {
        ChangeTurn(true,false);
    }

    private void OnDoVersus()
    {
        if(gameData.isPlayersTurn)
        {
            if((playerData.up && rivalData.up) || (playerData.down && rivalData.down) || (playerData.left && rivalData.left) || (playerData.right && rivalData.right) || playerData.center && rivalData.center)
            {
                SelectHole();
                DoNotDamageToRival();
                return;
            }


            else 
            {
                PlayerBallSpawn();
                SelectHole();
                DoDamageToRival();
                return;
            }
                
        }

        if(gameData.isRivalsTurn)
        {
            if((playerData.up && rivalData.up) || (playerData.down && rivalData.down) || (playerData.left && rivalData.left) || (playerData.right && rivalData.right) || playerData.center && rivalData.center)
            {
                SelectHole();
                DoNotDamageToPlayer();
                return;
            }
            

            else
            {
                RivalBallSpawn();
                SelectHole();
                DoDamageToPlayer();
                return;
            }
                
        }

    }

    private void SpawnBall(int index,Transform target)
    {
        GameObject ball=Instantiate(Ball,SpawnPos[index].position,Ball.transform.rotation);
        EventManager.Broadcast(GameEvent.OnBallSpawn);
        SpawnPos[index].GetChild(0).GetComponent<ParticleSystem>().Play();
        ball.transform.DOScale(Vector3.one*4,1f);
        ball.transform.DOLocalJump(new Vector3(target.position.x,target.position.y,target.position.z),1,1
        ,1f).OnComplete(()=>Destroy(ball));
        
    }

    private void SelectHole()
    {
        if(gameData.isPlayersTurn)
        {
            if(rivalData.left) ClosedHoles[0].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(rivalData.up) ClosedHoles[1].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(rivalData.right) ClosedHoles[2].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(rivalData.down) ClosedHoles[3].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(rivalData.center) ClosedHoles[4].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f); 
        }

        if(gameData.isRivalsTurn)
        {
            if(playerData.left) ClosedHoles[0].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(playerData.up) ClosedHoles[1].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(playerData.right) ClosedHoles[2].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(playerData.down) ClosedHoles[3].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f);
            if(playerData.center) ClosedHoles[4].transform.DOScale(new Vector3(0.9f,0.9f,0.1f),0.5f); 
        }
    }

    private void OnResetHoles()
    {
        for (int i = 0; i < ClosedHoles.Count; i++)
        {
            Debug.Log("WORK WORK");
            ClosedHoles[i].transform.DOScale(Vector3.zero,0.1f);
        }
    }
    private void RivalBallSpawn()
    {
        if(rivalData.left) SpawnBall(0,PlayerTarget);
        if(rivalData.up) SpawnBall(1,PlayerTarget);
        if(rivalData.right) SpawnBall(2,PlayerTarget);
        if(rivalData.down) SpawnBall(3,PlayerTarget);
        if(rivalData.center) SpawnBall(4,PlayerTarget);
    }

    private void PlayerBallSpawn()
    {
        if(playerData.left) SpawnBall(0,RivalTarget);
        if(playerData.up) SpawnBall(1,RivalTarget);
        if(playerData.right) SpawnBall(2,RivalTarget);
        if(playerData.down) SpawnBall(3,RivalTarget);
        if(playerData.center) SpawnBall(4,RivalTarget);
    }

    private void DoDamageToRival()
    {
        //Burada bunu yapmayacagiz. Instantiate ball olayi burada olacak. Ve Ball Rival'a Gidicek. Ball Obstacle Collide yapinca orada bu alttakileri yapacagiz.
        /*EventManager.Broadcast(GameEvent.OnTakeRivalDamage);
        Debug.Log("DAMAGE RIVAL");
        ChangeTurn(false,true);
        playerAnima.SetTrigger("NotDamage");
        EventManager.Broadcast(GameEvent.OnRivalsTurn);*/
    }

    private void DoNotDamageToRival()
    {
        EventManager.Broadcast(GameEvent.OnPreventRivalDamage);
        Debug.Log("NOT DAMAGE RIVAL");
        playerAnima.SetTrigger("Fail");
        //ChangeTurn(false,true);
        EventManager.Broadcast(GameEvent.OnRivalsTurn);
    }

    private void DoDamageToPlayer()
    {
        /*EventManager.Broadcast(GameEvent.OnTakePlayerDamage);
        Debug.Log("DAMAGE PLAYER");
        ChangeTurn(true,false);
        rivalAnim.SetTrigger("NotDamage");
        EventManager.Broadcast(GameEvent.OnPlayersTurn);*/
    }

    private void DoNotDamageToPlayer()
    {
        EventManager.Broadcast(GameEvent.OnPreventPlayerDamage);
        Debug.Log("NOT DAMAGE PLAYER");
        rivalAnim.SetTrigger("Fail");
        //ChangeTurn(true,false);
        EventManager.Broadcast(GameEvent.OnPlayersTurn);
    }


    private void ChangeTurn(bool playerTurn,bool rivalTurn)
    {
        gameData.isPlayersTurn=playerTurn;
        gameData.isRivalsTurn=rivalTurn;
    }
    
}
