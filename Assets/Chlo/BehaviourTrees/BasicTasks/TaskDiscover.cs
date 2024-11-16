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

    
        //WAIT FOR ANIMATIONS FIRST
    /*
    private float waitTime;
    private bool waitForPreviousNode = false;
   */
   
   
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
           if (_enemyContainer.discoverChoices.Count < 1)
           {
               for (int j = 0; j < 3; j++)
               {
                   _enemyContainer.discoverChoices.Add( currentPool.ElementAt(Random.Range(0, currentPool.Count - 1)));
               }    
                            
           }

           state = NodeState.SUCCESS;
           return state; 
   }
}
