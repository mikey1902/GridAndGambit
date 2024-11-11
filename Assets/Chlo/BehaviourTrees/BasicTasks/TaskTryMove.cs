using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskTryMove : BTNode
{

    private EnemyContainer _enemyContainer;
    private Card ChosenCard;
    public float waitCounter;
    private float _waitTime;

    private Card selectedCard;
    private int reps;
 
    public Transform _transform;
    private bool waitingForPreviousNode;


    public TaskTryMove(Transform unit, Card selectCard, EnemyContainer enemyContainer, float waitTime)
    {
      //  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
      _enemyContainer = enemyContainer;
      ChosenCard = selectCard;
      waitCounter = 0f;
      _waitTime = waitTime;
      _transform = unit;
      waitingForPreviousNode = true;
}    


    public override NodeState Evaluate()
    {
      
           Debug.Log("TaskStarted");
       
            state = NodeState.FAILURE;
            return state;
     

    
    }

}