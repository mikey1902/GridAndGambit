using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskPlayCard : BTNode
{

    private EnemyContainer _enemyContainer;
    private Card ChosenCard;
    public float waitCounter = 0f;
    private float _waitTime;

    private Card selectedCard;
    private int reps;
    [SerializeField] private GridGambitProd.Card.CardType _cardType;
    public Transform _transform;
    private bool waitingForPreviousNode = false;


    public TaskPlayCard(Transform unit, Card selectCard, EnemyContainer enemyContainer, float waitTime)
    {
      //  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
      _enemyContainer = enemyContainer;
      ChosenCard = selectCard;
      _cardType = ChosenCard.cardType;
      _waitTime = waitTime;
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
         }
         else
         {
           switch (_cardType)
           {
               case Card.CardType.Attack:

                   break;
               case Card.CardType.Support:

                   break;
               
               default:
                   Debug.Log("wth boi, what u doin - Not implemented yet");
                   break;
           }
           Debug.Log(selectedCard.cardType.ToString()); 
           _enemyContainer.cardToPlay = selectedCard;
         }
         state = NodeState.SUCCESS;
        return state;
    }
    
    
}
