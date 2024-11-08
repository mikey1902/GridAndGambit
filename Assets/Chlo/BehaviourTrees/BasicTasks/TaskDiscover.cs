using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskDiscover : BTNode
{
   /* public TaskSearch(Vector2 currentPosition, Vector2[] waypoints)
    {
        currentPosition = gameObject.
    }*/
   
 
    private List<SpellCard> currentPool;
    private int reps;
 
    
    public float waitCounter = 0f;
    private float waitTime = 1f;
    private bool waitingForDiscover = false;
   
   
   
   public TaskDiscover(Transform unit, List<SpellCard> cardPool, int repeatNum)
   {
      reps = repeatNum;
      currentPool = cardPool;
   }
    public override NodeState Evaluate()
    {
        for(var i = 0; i < reps; i++)
        if (waitingForDiscover)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
                waitingForDiscover = false;
        }
        else
        {
                  /*  Transform targetTransform = _waypoints[currentWalkTargetIndex];
                    if (Vector3.Distance(_transform.position, targetTransform.position) < 0.1f)
                    {
                        //_transform.position = targetTransform.position;
                        waitCounter = 0f;
                        waitingForDiscover = true;
                        
                        //currentWalkTargetIndex = (currentWalkTargetIndex + 1) % _waypoints.Length;
                    }
                    else
                    {
                        _transform.position = Vector3.MoveTowards(_transform.position, targetTransform.position, Time.deltaTime * ToySoldierBTree.speed);
                        
                    }*/
        }

        

        state = NodeState.RUNNING;
        return state;

    }
    
    
}
