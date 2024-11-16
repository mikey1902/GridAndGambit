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
    private float waitCounter =0f;
    private BattleManager _battleManager;
    private float _waitTime = 2f;
    //WAIT FOR ANIMATIONS FIRST
    /*
    private float waitTime;
    private bool waitForPreviousNode = false;
   */
   
   
    public TaskRealPlay(BattleManager battleManager, EnemyContainer container, float waitTime)
    {
        _card  = container.CardToPlay;
        _target = container.Target; 
        waitForPreviousNode = true;
        _waitTime = waitTime;
        _battleManager = battleManager;
    }

    public override NodeState Evaluate()
    {
        if (waitForPreviousNode)
              {
                  waitCounter += Time.deltaTime;
                  if (waitCounter >= _waitTime)
                      waitForPreviousNode = false;
              }else {
                  //BODY - TALK TO MIKE
                   switch (_card.cardType)
                    {
                        case Card.CardType.Attack:
                            _battleManager.AttackCardEffect(_card as AttackCard, _target.gameObject);         
                            break;
                        case Card.CardType.Support:
                            _battleManager.SupportCardEffect(_card as SupportCard, _target.gameObject);
                            break;
                        default:
                            Debug.Log("wth boi, what u doin - Not implemented yet");
                            break;
                    }
       
        
        
                            
        

        state = NodeState.SUCCESS;
        return state; 
    }
        state = NodeState.RUNNING;
        return state; 
    
    }
    }