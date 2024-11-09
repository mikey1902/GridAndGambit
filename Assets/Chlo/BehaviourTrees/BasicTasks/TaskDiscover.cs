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
   /* public TaskSearch(Vector2 currentPosition, Vector2[] waypoints)
    {
        currentPosition = gameObject.
    }*/

    public List<Card> selectedCards = new List<Card>();
    private List<Card> currentPool;
    private EnemyContainer _enemyContainer;
    private int reps;

    private float _waitTime;
    public Transform _transform;
    public float waitCounter = 0f;
    private float waitTime = 1f;
    private bool waitForPreviousNode = false;
   
   
   
   public TaskDiscover(Transform unit,  int repeatNum, EnemyContainer enemyContainer, float waitTime, params string[] discoverStr)
   {
      reps = repeatNum;
      _waitTime = waitTime;
      currentPool = ReturnCardPool(true, discoverStr);
      _enemyContainer = enemyContainer;
      _transform = unit;
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

               // _transform.gameObject.GetComponent<EnemyContainer>().discoverChoices.Add()
               //  }
               state = NodeState.SUCCESS;
               return state;
           }
           
   //    }
 state = NodeState.FAILURE;
           return state;
   }
}
