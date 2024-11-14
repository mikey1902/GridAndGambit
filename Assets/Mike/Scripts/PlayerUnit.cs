using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

public class PlayerUnit : MonoBehaviour
{
	public List<Card> CardsInResources = new List<Card>();
	public string CDTypeFolderName;
	public UnitMoveType unitMoveType;
	public int moveDistance = 2;
	private GridManager gridManager;
	private DrawManager drawManager;
	private HandManager handManager;

	public bool discoverReady = true;
	public bool moveReady = true;

	public enum UnitMoveType
	{
		Orthogonal,
		Diagonal,
		LShape,
	}

	void Awake()
	{
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
	}

	public void DiscoverSetup()
	{
		handManager.MaxHandSizeSetup(3);
		drawManager.CreateUnitDeck(CardsInResources);
		drawManager.FirstHandSetup(3);
		discoverReady = false;
	}
	public bool MoveSetup(Vector2 cellPos)
	{
		moveReady = false;
		switch (unitMoveType)
		{
			case UnitMoveType.Orthogonal:
				gridManager.OrthogonalMovement(cellPos, moveDistance);
				return true;

			case UnitMoveType.Diagonal:
				gridManager.DiagonalMovement(cellPos, moveDistance);
				return true;

			case UnitMoveType.LShape:
				gridManager.LShapeMovement(cellPos, moveDistance);
				return true;
		}
		return false;
	}
}
