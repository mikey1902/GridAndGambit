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
   
   private Transform _transform;
   private Transform[] _waypoints;
   
    private int currentWalkTargetIndex = 0;
    public float waitCounter = 0f;
    private float waitTime = 1f;
    private bool waitingForMove = false;
    
   
   
   public TaskSearch(Transform pos, Transform[] waypoints)
   {
       _transform = pos;
       _waypoints = waypoints;
   }
    public override NodeState Evaluate()
    {
        if (waitingForMove)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
             waitingForMove = false;
        }
        else
        {
                    Transform targetTransform = _waypoints[currentWalkTargetIndex];
                    if (Vector3.Distance(_transform.position, targetTransform.position) < 0.1f)
                    {
                        _transform.position = targetTransform.position;
                        waitCounter = 0f;
                        waitingForMove = true;
                        
                        currentWalkTargetIndex = (currentWalkTargetIndex + 1) % _waypoints.Length;
                    }
                    else
                    {
                        _transform.position = Vector3.MoveTowards(_transform.position, targetTransform.position, Time.deltaTime * ToySoldierBTree.speed);
                        
                    }
        }

        

        state = NodeState.RUNNING;
        return state;

    }
    
    
}
