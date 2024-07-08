using GridGambitProd;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
	public List<Card> allCards = new List<Card>();

	public int startingHandSize = 6;

	private int currentIndex = 0;
	public int maxHandSize;
	public int currentHandSize;
	private HandManager handManager;

	void Start()
	{
		//loads all card datas from resources folder
		Card[] cardLibrary = Resources.LoadAll<Card>("CardData");

		//add the loaded cards to the card list
		allCards.AddRange(cardLibrary);

		handManager = FindObjectOfType<HandManager>();
		maxHandSize = handManager.maxHandSize;
		for (int i = 0; i < startingHandSize; i++)
		{
			DrawCard(handManager);
		}
	}

	private void Update()
	{
		if(handManager != null)
		{
			currentHandSize = handManager.cardsInHand.Count;
		}
	}

	public void DrawCard(HandManager handManager)
	{
		if (allCards.Count == 0)
			return;

		if (currentHandSize < maxHandSize)
		{
			Card nextCard = allCards[currentIndex];
			handManager.AddCard(nextCard);

			//mod to never go beyond how many cards are in the list
			currentIndex = (currentIndex + 1) % allCards.Count;
		}
	}
}
