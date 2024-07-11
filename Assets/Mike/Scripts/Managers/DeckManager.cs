using GridGambitProd;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
	public List<Card> allCards = new List<Card>();

	public int startingHandSize = 6;
	public int maxHandSize = 6;
	public int currentHandSize;

	private HandManager handManager;
	private DrawManager drawManager;

	private bool startMatch = true;

	void Start()
	{
		//load cards from resources folder
		Card[] cards = Resources.LoadAll<Card>("CardData");

		allCards.AddRange(cards);
	}

	void Awake()
	{
		if (drawManager == null)
		{
			drawManager = FindObjectOfType<DrawManager>();
		}
		if (handManager == null)
		{
			handManager = FindObjectOfType<HandManager>();
		}
	}

	void Update()
	{
		if(startMatch)
		{
			MatchSetup();
		}
	}

	public void MatchSetup()
	{
		handManager.MaxHandSizeSetup(maxHandSize);
		drawManager.CreateDrawPile(allCards);
		drawManager.FirstHandSetup(startingHandSize, maxHandSize);
		startMatch = false;
	}
}
