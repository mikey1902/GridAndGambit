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
    public Card[] cds;

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
      
            waitCounter += Time.deltaTime;
            if (waitCounter > _waitTime)
                waitingForPreviousNode = false;
        }
        else if(!waitingForPreviousNode && _greed == false)
        {
            
                for (var i = 0; i < _enemyContainer.discoverChoices.Length; i++)
                {
                    cds[i] = _enemyContainer.discoverChoices[i];
                    switch (cds[i].cardType)
                    {
                        case Card.CardType.Attack:
                            Debug.Log("attack");

                            AttackCard atkC = (AttackCard)cds[i];
                            cds[i].cardScore = atkC.damage;
                            //cds[i].Typing = 0;
                            break;
                        case Card.CardType.Support:
                            Debug.Log("support");

                            SupportCard supC = (SupportCard)cds[i];
                            cds[i].cardScore = (supC.supportAmount + supC.range);
                            //cds[i].Typing = 1;

                            break;
                        case Card.CardType.Move:
                            Debug.Log("move");

                            MoveCard mveC = (MoveCard)cds[i];
                            cds[i].cardScore = (mveC.moveDistance);
                            //cds[i].Typing = 2;
                            break;

                        default:
                            Debug.Log("wth boi, what u doin - Not implemented yet");
                            break;
                    }
                }

                //_enemyContainer.Target = GameObject.Find("FriendlyUnit").transform;
                _enemyContainer.CardToPlay = cds[0];
                //Debug.Log("the cards name " + _enemyContainer.CardToPlay.cardType);
                _enemyContainer.discoverCard =_enemyContainer.CardToPlay;
                state = NodeState.SUCCESS;
                return state;
            }
        //Debug.Log("greed switch is real");
        state = NodeState.FAILURE;
        return state;
            }
    }
