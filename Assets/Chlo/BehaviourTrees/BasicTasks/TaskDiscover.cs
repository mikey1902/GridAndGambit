using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;
using static GridGambitProd.GridGambitUtil;
using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskDiscover : BTNode
{
   private List<Card> currentPool;
    private EnemyContainer _enemyContainer;

    private float waitTime;
    private bool waitForPreviousNode = false;
   
   
   
   public TaskDiscover(EnemyContainer enemyContainer, params string[] discoverStr)
   {
      currentPool = ReturnCardPool(true, discoverStr);
      _enemyContainer = enemyContainer;
   }

   public override NodeState Evaluate()
   {
    /*   if (waitForPreviousNode)
       {
           waitCounter += Time.deltaTime;
           if (waitCounter >= _waitTime)
               waitForPreviousNode = false;
       }*/
     /*  else
       {*/
           if (!_enemyContainer.discoverChoices[0])
           {
               for (int j = 0; j < 3; j++)
               {
                   _enemyContainer.discoverChoices[j] = currentPool.ElementAt(Random.Range(0, currentPool.Count - 1));
               }    
                        
           }

          // Debug.Log("TaskDiscover Success");
           state = NodeState.SUCCESS;
           return state; 
   }
}
