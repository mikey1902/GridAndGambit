using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using TMPro;

public class DiscardManager : MonoBehaviour
{
	public List<Card> discardCards = new List<Card>();
	public TextMeshProUGUI discardCount;
	public int discardCardsCount;

	void Awake()
	{
		UpdateDiscardCount();
	}

	private void UpdateDiscardCount()
	{
		discardCount.text = discardCards.Count.ToString();
		discardCardsCount = discardCards.Count;
	}

	public void AddCardToDiscard(Card card)
	{
		if(card != null)
		{
			discardCards.Add(card);
			UpdateDiscardCount();
		}
	}

	public List<Card> PullAllCardsInDiscard()
	{
		if (discardCards.Count > 0)
		{
			List<Card> cardsToReturn = new List<Card>(discardCards);
			discardCards.Clear();
			UpdateDiscardCount();
			return cardsToReturn;
		}
		else
		{
			return new List<Card>();
		}
	}
}
