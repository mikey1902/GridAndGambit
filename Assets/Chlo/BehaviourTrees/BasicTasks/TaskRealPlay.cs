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

    private float _waitTime = 2f;
    //WAIT FOR ANIMATIONS FIRST
    /*
    private float waitTime;
    private bool waitForPreviousNode = false;
   */
   
   
    public TaskRealPlay(EnemyContainer container)
    {
        _card  = container.CardToPlay;
        _target = container.Target; 
        waitForPreviousNode = true;
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
                  
                  
                  Debug.Log("waitin ");
                  
       
        
        
                            
        

        state = NodeState.SUCCESS;
        return state; 
    }
        state = NodeState.RUNNING;
        return state; 
    
    }
    }