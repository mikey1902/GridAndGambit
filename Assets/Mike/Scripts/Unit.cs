using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

public class Unit : MonoBehaviour
{
	public List<Card> CardsInResources = new List<Card>();
	public bool isDestroyed = false;
	public bool hasActed = false;
	public string CDTypeFolderName;
	private GridManager gridManager;
	private DrawManager drawManager;
	private HandManager handManager;

	private bool discoverReady = true;
	private LayerMask unitLayerMask;

	void Awake()
	{
		unitLayerMask = LayerMask.GetMask("Units");
		gridManager = FindObjectOfType<GridManager>();
		handManager = FindObjectOfType<HandManager>();
		drawManager = GetComponent<DrawManager>();
		
	}
	void Start()
	{
		//load cards from resources folder
		Card[] cards = Resources.LoadAll<Card>(CDTypeFolderName);

		CardsInResources.AddRange(cards);
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
		handManager.MaxHandSizeSetup(3);
		drawManager.CreateUnitDeck(CardsInResources);
		drawManager.FirstHandSetup(3);
		discoverReady = false;
	}
	public void DestroyUnit()
	{
		isDestroyed = true;
		if (isDestroyed)
		{
			Destroy(gameObject);
		}
	}
}
