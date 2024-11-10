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
    private float _waitTime;
    private bool waitingForPreviousNode = false;


    public TaskCheckCard(Transform unit, EnemyContainer enemyContainer, float waitTime)
    {
        //  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
        waitingForPreviousNode = true;
        _enemyContainer = enemyContainer;
        _transform = unit;
        _waitTime = waitTime;

//        Debug.Log(selectedCard);
    }


    public override NodeState Evaluate()
    {
        Debug.Log(waitCounter);
        if (waitingForPreviousNode)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter > _waitTime)
                waitingForPreviousNode = false;
        }
        else
        {
            //Random.Range(0, _enemyContainer.discoverChoices.Length
            selectedCard = _enemyContainer.discoverChoices[0];
            Debug.Log(selectedCard);

            foreach (var VARIABLE in _enemyContainer.discoverChoices)
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

                _enemyContainer.CardToPlay = selectedCard;
                Debug.Log("Finished TaskCheckCard");
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;





    }
}