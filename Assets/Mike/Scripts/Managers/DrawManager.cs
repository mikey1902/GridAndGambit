using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using TMPro;

public class DrawManager : MonoBehaviour
{
	public List<Card> UnitDeck = new List<Card>();

	public int startingHandSize;

	private int currentIndex = 0;
	public int maxHandSize;
	public int currentHandSize;
	private HandManager handManager;

	void Start()
	{
		handManager = FindObjectOfType<HandManager>();
	}

	void Update()
	{
		if (handManager != null)
		{
			currentHandSize = handManager.cardsInHand.Count;
		}
	}

	public void CreateUnitDeck(List<Card> cardsToAdd)
	{
		UnitDeck.AddRange(cardsToAdd);
	}

	public void FirstHandSetup(int HandSize)
	{
		maxHandSize = HandSize;
		for(int i = 0; i < HandSize; i++)
		{
			DrawCard(handManager);
		}
	}

	public void DrawCard(HandManager handManager)
	{

		if (currentHandSize < maxHandSize)
		{
			Card nextCard = UnitDeck[currentIndex];
			handManager.AddCard(nextCard, gameObject);
			UnitDeck.RemoveAt(currentIndex);

			if (UnitDeck.Count > 0)
			{
				currentIndex %= UnitDeck.Count;
			}
		}
	}
}
