using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using TMPro;

public class DrawManager : MonoBehaviour
{
	public List<Card> drawPileCards = new List<Card>();

	public int startingHandSize;

	private int currentIndex = 0;
	public int maxHandSize;
	public int currentHandSize;
	private HandManager handManager;
	private DiscardManager discardManager;
	public TextMeshProUGUI drawPileCount;

	void Start()
	{
		handManager = FindObjectOfType<HandManager>();
		discardManager = FindObjectOfType<DiscardManager>();
	}

	void Update()
	{
		if (handManager != null)
		{
			currentHandSize = handManager.cardsInHand.Count;
		}
	}

	public void CreateDrawPile(List<Card> cardsToAdd)
	{
		drawPileCards.AddRange(cardsToAdd);
		//Utility.Shuffle(drawPileCards);
		UpdateDrawPileNumber();
	}

	public void FirstHandSetup(int numberOfDrawCards, int setHandSize)
	{
		maxHandSize = setHandSize;
		for(int i = 0; i < numberOfDrawCards; i++)
		{
			DrawCard(handManager);
		}
	}

	public void DrawCard(HandManager handManager)
	{
		if (drawPileCards.Count == 0)
		{
			ShuffleDiscardIntoDeck();
		}

		if (currentHandSize < maxHandSize)
		{
			Card nextCard = drawPileCards[currentIndex];
			handManager.AddCard(nextCard);
			drawPileCards.RemoveAt(currentIndex);
			UpdateDrawPileNumber();

			if (drawPileCards.Count > 0)
			{
				currentIndex %= drawPileCards.Count;
			}
		}
	}

	private void ShuffleDiscardIntoDeck()
	{
		if (discardManager == null)
		{
			discardManager = FindObjectOfType<DiscardManager>();
		}

		if (discardManager != null && discardManager.discardCardsCount > 0)
		{
			drawPileCards = discardManager.PullAllCardsInDiscard();
			//Utility.Shuffle(drawPileCards);
			currentIndex = 0;
		}
	}

	private void UpdateDrawPileNumber()
	{
		drawPileCount.text = drawPileCards.Count.ToString();
	}
}
