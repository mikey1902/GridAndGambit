using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


public class TaskPlayCard : BTNode
{
   /* public TaskSearch(Vector2 currentPosition, Vector2[] waypoints)
    {
        currentPosition = gameObject.
    }*/

   public List<Card> selectedCards;
    private Card selectedCard;
    private int reps;
 
    public Transform _transform;
    public float waitCounter = 0f;
    private float waitTime = 1f;
    private bool waitingForDiscover = false;



    public TaskPlayCard(Transform unit)
    {
        selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
        selectedCard = selectedCards.ElementAt(Random.Range(0, selectedCards.Count));

    }


    public override NodeState Evaluate()
    {
       // for(var i = 0; i < reps; i++)
         if (waitingForDiscover)
         {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
                waitingForDiscover = false;
         }
         else
         {
           //Code for card Playing

           switch (selectedCard.cardType)
           {
               case Card.CardType.Attack:

                   break;
               case Card.CardType.Support:

                   break;
               
               default:
                   Debug.Log("wth boi, what u doin - Not implemented yet");
                   break;
           }
           
         }
         state = NodeState.RUNNING;
        return state;
    }
    
    
}
