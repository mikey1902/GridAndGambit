using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using JetBrains.Annotations;

public class EnemyContainer : MonoBehaviour
{
    public bool isPlayingCard;
    public bool moveReady;
    public GridManager gridManager;
    public List<Card> discoverChoices;
    public Card CardToPlay;
    public Card discoverCard;
    public Transform Target;
    public MoveManager moveManager;
    private void Awake()
    { 
        discoverChoices = new List<Card>();
        gridManager = FindObjectOfType<GridManager>();
        moveManager = FindObjectOfType<MoveManager>();
    }

}
