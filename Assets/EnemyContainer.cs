using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using JetBrains.Annotations;

public class EnemyContainer : MonoBehaviour
{
    public bool isPlayingCard = false;
    public Card[] discoverChoices;
    public CardInfo CardToPlay;
    public Card discoverCard;
    public CardInfo[] CardInfos;
    public Transform Target;
    private void Awake()
    { 
        discoverChoices = new Card[3];
        CardInfos = new CardInfo[3];
        
    }

}
