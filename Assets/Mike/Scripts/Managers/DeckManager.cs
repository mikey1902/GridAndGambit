using GridGambitProd;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
	public List<Card> allCardsInResources = new List<Card>();

	public int startingHandSize = 3;
	public int maxHandSize = 3;
	public int currentHandSize;

	private HandManager handManager;
	private DrawManager drawManager;

	private bool discoverReady = true;
	private LayerMask unitLayerMask;

	void Start()
	{
		//load cards from resources folder
		Card[] cards = Resources.LoadAll<Card>("CardData");

		allCardsInResources.AddRange(cards);
	}

	void Awake()
	{
		unitLayerMask = LayerMask.GetMask("Units");

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
		if (Input.GetMouseButtonUp(0) && discoverReady == true)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);
			if (hit.collider != null && hit.collider.GetComponent<Unit>().hasActed == false)
			{
				DiscoverSetup();
				hit.collider.GetComponent<Unit>().hasActed = true;
			}
		}
	}

	public void DiscoverSetup()
	{
		handManager.MaxHandSizeSetup(maxHandSize);
		drawManager.CreateUnitDeck(allCardsInResources);
		drawManager.FirstHandSetup(startingHandSize, maxHandSize);
		discoverReady = false;
	}
}
