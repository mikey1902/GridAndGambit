using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using JetBrains.Annotations;
using UnityEngine;

public class ToySoldierBTree : BTree
{
   // public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;
    public string[] relatedCardPools;
    private GridManager gridManager;
    public EnemyContainer container;
    
     void Awake()
     {
         gridManager = gameObject.GetComponent<EnemyContainer>().gridManager;
         container = this.GetComponent<EnemyContainer>();
     }
//LAZY UNIT
    protected override BTNode SetupTree()
    {
        /*BTNode root = new TaskSearch(transform, waypoints);
        return root;*/
        BTNode root = new Sequence(new List<BTNode>
        {
            new TaskDiscover(container, relatedCardPools),
            new Selector(new List<BTNode>{
         // new TaskCheckCard(transform, container, 5f),
            new Sequence(new List<BTNode>
           { 
               new TaskCheckCard(transform, container, 2f, false),
           }),
            new Sequence(new List<BTNode>
            {
                //Code for Moving closer to Target
                //Create a task to check container
                new TaskCheckCard(transform, container, 2f, true),//CHECKS EACH CARD AND ORDERS THEM BY SCORE
                //- READS DISCOVER LIST AND ATTEMPTS TO PLAY EACH LOOKING FROM FIRST DECENDING
                //IF NONE CURRENTLY PLAYABLE, RETURNS FAILURE, WHICH MOVES TO NEXT SEQUENCE
            }),
             }),
            new TaskPlayCard( container.Target, transform, container, 3f),
            new TaskMove(container, 3, gridManager, container.Target, 3f),
            new TaskRealPlay(container.battleManager, container, 3f),
        });
             
        return root;
        
        
        
        
               /* new Sequence(new List<BTNode> {
        new TaskCheckCard(transform, container)
        });*/

        //new TaskTryMoving(transform, container, )
        // new TaskSearch(transform, waypoints),
        //
        //new TaskCheckCard()

        /*new Sequence(new List<BTNode> {

            //new TaskTryMove(transform, )
        }),
        //break out of sequence if can't play cards. or decide whether or not to try and move first (currently random)
      //  new TaskPlayCard(transform, container.cardToPlay, container, 1f),
      /*  new Sequence(new List<BTNode> {
            //

            //new TaskEndTurn
            }),*/

        // new TaskSearch(transform, waypoints),

   
    }
}
