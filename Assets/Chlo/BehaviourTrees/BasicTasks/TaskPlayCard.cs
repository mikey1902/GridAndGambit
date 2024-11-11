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

    private Transform _target;
    private CardInfo selectedCard;
    private int reps;
 
    public Transform _transform;
    private bool waitingForPreviousNode;


    public TaskPlayCard(Transform target, Transform unit, CardInfo selectCard, EnemyContainer enemyContainer, float waitTime)
    {
      //  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
      _enemyContainer = enemyContainer;
      ChosenCard = selectCard;
      waitCounter = 0f;
      _waitTime = waitTime;
      _transform = unit;
      waitingForPreviousNode = true;
     _target = target;
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
        //    _transform.gameObject.SetActive(false);
            
       
            
        ///    Debug.Log(ChosenCard.Typing);
           switch(ChosenCard.Typing)
            {
             case 0://Attack
               AttackCard atc = ChosenCard.smpCard as AttackCard; 
                 Debug.Log(atc.damage); 
                 //_target.gameObject.GetComponent<simpleSetup>().HP -= atc.damage;
                 break;   
            
               case 1://Support 
                   SupportCard stc = ChosenCard.smpCard as SupportCard;
                   Debug.Log(stc.supportAmount); 

                  // Debug.Log(stc.supportAmount); 

                 //  _target.gameObject.GetComponent<simpleSetup>().HP += stc.supportAmount;
                  break;      
               
           /*  case 2: //Move
               //  trg.HP -= mtc.damage; 
                 MoveCard mtc = (MoveCard)ChosenCard.smpCard; 
                 Debug.Log(mtc.moveDistance); 
                 break;*/
             
              
             default:
                     Debug.Log("Chosen card is unknown");
                 break;
            }


            
            
            
            
            
            
            
            
            
            
            
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
