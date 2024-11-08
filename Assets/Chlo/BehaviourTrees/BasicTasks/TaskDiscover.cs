using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskDiscover : BTNode
{
   /* public TaskSearch(Vector2 currentPosition, Vector2[] waypoints)
    {
        currentPosition = gameObject.
    }*/

   public List<Card> selectedCards;
    private List<Card> currentPool;
    private int reps;
 
    public Transform _transform;
    public float waitCounter = 0f;
    private float waitTime = 1f;
    private bool waitingForDiscover = false;
   
   
   
   public TaskDiscover(Transform unit,  int repeatNum, params string[] discoverStr)
   {
      reps = repeatNum;
      if (discoverStr.Length > 1)
      { 
          currentPool = new List<Card>();
         foreach(string item in discoverStr)
         {
           currentPool = Enumerable.Union(currentPool, Resources.LoadAll<Card>(item)).ToList();
         }
      }
      else
      {
          currentPool = Resources.LoadAll<Card>(discoverStr[0]).ToList();
      }
      selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
      _transform = unit;
   }
   
    public override NodeState Evaluate()
    {
       // for(var i = 0; i < reps; i++)
         if (waitingForDiscover)
         {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
                waitingForDiscover = false;
         }
         else
         {
             for (int j = 0; j < 3; j++)
             {
                 selectedCards.Add(currentPool.ElementAt(Random.Range(0, currentPool.Count)));
             } 
         }
         state = NodeState.RUNNING;
        return state;
    }
    
    
}
