using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskCheckCard : BTNode
{

    private bool _greed;
    private int reps;
    private EnemyContainer _enemyContainer;
    public Transform _transform;
    public float waitCounter = 0f;
    private float _waitTime;
    private bool waitingForPreviousNode = false;
    public CardInfo[] cds;

    public TaskCheckCard(Transform unit, EnemyContainer enemyContainer, float waitTime, bool greed)
    {
        //  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
        waitCounter = 0f;
        waitingForPreviousNode = true;
        _enemyContainer = enemyContainer;
        _transform = unit;
        _waitTime = waitTime;
        cds = enemyContainer.CardInfos;
        _greed = greed;
    }


    public override NodeState Evaluate()
    {
        if (waitingForPreviousNode)
        {
            Debug.Log(waitCounter);
            waitCounter += Time.deltaTime;
            if (waitCounter > _waitTime)
                waitingForPreviousNode = false;
        }
        else if(!waitingForPreviousNode && _greed == false)
        {
            
            

                for (var i = 0; i < _enemyContainer.discoverChoices.Length; i++)
                {
                    cds[i] = new CardInfo { GenCard = _enemyContainer.discoverChoices[i] };
                    switch (cds[i].GenCard.cardType)
                    {
                        case GridGambitProd.Card.CardType.Attack:
                            AttackCard atkC = (AttackCard)cds[i].GenCard;
                            cds[i].Score = atkC.damage;
                            cds[i].Typing = 0;
                            break;
                        case GridGambitProd.Card.CardType.Support:

                            SupportCard supC = (SupportCard)cds[i].GenCard;
                            cds[i].Score = (supC.supportAmount + supC.range);
                            cds[i].Typing = 1;
                            break;
                        case GridGambitProd.Card.CardType.Move:

                            MoveCard mveC = (MoveCard)cds[i].GenCard;
                            cds[i].Score = (mveC.moveDistance);
                            cds[i].Typing = 2;
                            break;

                        default:
                            Debug.Log("wth boi, what u doin - Not implemented yet");
                            break;
                    }
                }

                _enemyContainer.CardToPlay = cds[0];
                Debug.Log("Finished TaskCheckCard");
                state = NodeState.SUCCESS;
                return state;
            }Debug.Log("greed switch is real");
                                     state = NodeState.FAILURE;
                                          return state;
            }
    }
