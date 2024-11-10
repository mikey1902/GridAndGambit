using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class ToySoldierBTree : BTree
{
    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;
    public string[] relatedCardPools;
    public EnemyContainer container;
     void Awake()
     {
         container = this.GetComponent<EnemyContainer>();
     }
//LAZY UNIT
    protected override BTNode SetupTree()
    {
        /*BTNode root = new TaskSearch(transform, waypoints);
        return root;*/
        BTNode root = new Selector(new List<BTNode>{
            new Sequence(new List<BTNode>
            {
                new TaskDiscover(transform, 1, container, 1f, relatedCardPools),
                new TaskCheckCard(transform, container, 1f),
                //new TaskPlayCard(transform, container.CardToPlay, container, 1f),

            }),
        new Sequence(new List<BTNode> {
        new TaskCheckCard(transform, container)
        });

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

    });
        return root;
        
    }
}
