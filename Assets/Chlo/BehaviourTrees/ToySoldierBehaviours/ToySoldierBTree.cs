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

    protected override BTNode SetupTree()
    {
        /*BTNode root = new TaskSearch(transform, waypoints);
        return root;*/
        BTNode root = new Sequence(new List<BTNode> {
            new TaskDiscover(transform, 1, container, 0f, "CardData"),
            new TaskCheckCard(transform, container, 1f), 
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
