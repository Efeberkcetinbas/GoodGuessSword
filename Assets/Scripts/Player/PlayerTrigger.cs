using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : Obstaceable
{
    [SerializeField] private ParticleSystem damageParticle;

    internal override void DoAction(BallTrigger Ball)
    {
        EventManager.Broadcast(GameEvent.OnTakePlayerDamage);
        Debug.Log("DAMAGE PLAYER");
        EventManager.Broadcast(GameEvent.OnPlayersTurn);
        EventManager.Broadcast(GameEvent.OnHit);
        damageParticle.Play();
    }
}
