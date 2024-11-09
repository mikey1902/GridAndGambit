using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskCheckCard : BTNode
{

    
    private Card selectedCard;
    private int reps;
    private EnemyContainer _enemyContainer;
    public Transform _transform;
    public float waitCounter = 0f;
    private float _waitTime = 1f;
    private bool waitingForPreviousNode = false;


    public TaskCheckCard(Transform unit, EnemyContainer enemyContainer, float waitTime)
    {
      //  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
      waitingForPreviousNode = true;
      _enemyContainer = enemyContainer;
      _transform = unit;
      
    }


    public override NodeState Evaluate()
    {
        
         if (waitingForPreviousNode)
         {
             //code for delay goes here
            waitCounter += Time.deltaTime;
            if (waitCounter >= _waitTime)
                waitingForPreviousNode = false;
            selectedCard = _enemyContainer.discoverChoices[Random.Range(0, _enemyContainer.discoverChoices.Length)];
         }
         else
         {
           switch (selectedCard.cardType)
           {
               case GridGambitProd.Card.CardType.Attack:
                   Debug.Log(selectedCard.cardType.ToString()); 

                   break;
               case GridGambitProd.Card.CardType.Support:
                   Debug.Log(selectedCard.cardType.ToString()); 

                   break;
               
               default:
                   Debug.Log("wth boi, what u doin - Not implemented yet");
                   break;
           }
           _enemyContainer.cardToPlay = selectedCard;
         }
         state = NodeState.SUCCESS;
        return state;
    }
    
    
}
