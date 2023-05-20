using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalTrigger : Obstaceable
{
    internal override void DoAction(BallTrigger Ball)
    {
        EventManager.Broadcast(GameEvent.OnTakeRivalDamage);
        Debug.Log("DAMAGE RIVAL");
        EventManager.Broadcast(GameEvent.OnRivalsTurn);
        EventManager.Broadcast(GameEvent.OnHit);
    }
}
