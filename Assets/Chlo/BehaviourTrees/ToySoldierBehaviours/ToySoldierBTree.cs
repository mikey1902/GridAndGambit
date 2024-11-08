using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class ToySoldierBTree : BTree
{
    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;
    protected override BTNode SetupTree()
    {
        BTNode root = new TaskSearch(transform, waypoints);
        return root;
        
    }
}
