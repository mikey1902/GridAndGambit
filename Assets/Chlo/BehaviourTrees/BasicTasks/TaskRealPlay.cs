using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;
using static GridGambitProd.GridGambitUtil;
using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskRealPlay : BTNode
{
    private Card _card;
    private Transform _target;
    private bool waitForPreviousNode;
    private float waitCounter;
    private BattleManager _battleManager;
    private float _waitTime;
    private EnemyContainer _container;

    private ToySoldierBTree _ts;
    //WAIT FOR ANIMATIONS FIRST
    /*
    private float waitTime;
    private bool waitForPreviousNode = false;
   */


    public TaskRealPlay(BattleManager battleManager, EnemyContainer container, float waitTime)
    {
        waitCounter = 0f;
        _container = container;
        waitForPreviousNode = true;
        _waitTime = waitTime;
        _battleManager = battleManager;
        _container = container;
        _ts = container.gameObject.GetComponent<ToySoldierBTree>();
    }

    public override NodeState Evaluate()
    {
        if (waitForPreviousNode)
        {
            Debug.Log(waitCounter);
            waitCounter += Time.deltaTime;
            if (waitCounter > _waitTime)
                waitForPreviousNode = false;
        }
        else if (waitForPreviousNode == false)
        {
            _card = _container.discoverCard;
            Debug.Log(_container.Target);
                switch (_card.cardType)
                {
                    case Card.CardType.Attack:
                        _battleManager.AttackCardEffect(_card as AttackCard, _container.Target.gameObject);
                        break;
                    case Card.CardType.Support:
                        _battleManager.SupportCardEffect(_card as SupportCard, _container.Target.gameObject);
                        break;
                    //case Card.CardType.Move:
                    //   _battleManager.MoveCardEffect(_card as MoveCard, _target.gameObject);
                    //  break;
                    default:
                        Debug.Log("wth boi, what u doin - Not implemented yet");
                        break;
                }

                Debug.Log("aaaa");
                _container.iveHadMyTurn = true;
                _ts.enabled = false;
                if (_container.iveHadMyTurn == true)
                {
                    state = NodeState.SUCCESS;
                    return state;
                }
            


        }

        state = NodeState.FAILURE;
        return state;

    }
}