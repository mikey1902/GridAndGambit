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
    private CardInfo ChosenCard;
    public float waitCounter;
    private float _waitTime;

    private CardInfo selectedCard;
    private int reps;
 
    public Transform _transform;
    private bool waitingForPreviousNode;


    public TaskPlayCard(Transform unit, CardInfo selectCard, EnemyContainer enemyContainer, float waitTime)
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
        if (waitingForPreviousNode)
        {
          
            //code for delay goes here
            waitCounter += Time.deltaTime;
            if (waitCounter >= _waitTime)
                waitingForPreviousNode = false;
            
        }
        else
        {
            _transform.gameObject.SetActive(false);
            /* else
             {
               switch (_cardType)
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
               }*/

            //  }
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
