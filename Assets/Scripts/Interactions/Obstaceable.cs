using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstaceable : MonoBehaviour
{
   float st = 0;
    internal float interval = 3;
    internal bool canStay=false;
    internal bool canInteract = true;
    internal bool canDamageToPlayer=true;
    internal string interactionTag = "Ball";

    void OnTriggerEnter(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            StartInteractWithEnemy(other.GetComponent<BallTrigger>());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            InteractWithEnemy(other.GetComponent<BallTrigger>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == interactionTag)
        {
            InteractionExit(other.GetComponent<BallTrigger>());
        }
    }

    void StartInteractWithEnemy(BallTrigger Ball)
    {
        DoAction(Ball);
    }

   

    void InteractWithEnemy(BallTrigger Ball)
    {
        st += Time.deltaTime;
        if (st > interval && canStay)
        {
            ResetProgress();
            DoAction(Ball);
        }
    }
    internal virtual void ResetProgress()
    {
        st = 0;
    }
    internal virtual void InteractionExit(BallTrigger Ball)
    {
        st = 0;
    }
    internal virtual void DoAction(BallTrigger Ball)
    {
        throw new System.NotImplementedException();
    }

    
    internal void StopInteract()
    {
        canInteract = false;
    }
    internal void StartInteract()
    {
        canInteract = true;
    }
}
